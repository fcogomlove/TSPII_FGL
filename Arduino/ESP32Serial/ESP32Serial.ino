#include "MeanFilterLib.h"

//Definir los pines del joystick
#define JOYSTICK_X 35
#define JOYSTICK_Y 34

char receivedChar;

//Instancia del objeto del filtro de ventana (10 muestras)
MeanFilter<long> meanFilter(10);

//Parametro de suavizado para el filtro
const float smoothFactor = 0.1;

//Variables para valores filtrados
float xValueFiltered = 0;
float yValueFiltered = 0;

void setup() {
  //Inicializa el puerto serial 9600

  Serial.begin(9600);

  //Configuracion de los pines del joystick

  pinMode(JOYSTICK_X, INPUT);
  pinMode(JOYSTICK_Y, INPUT);
  pinMode(2, OUTPUT);

}

void loop() {
  //Lectura de los valores de los ejes X y Y del joystick
  
  int xValue = analogRead(JOYSTICK_X);
  int yValue = analogRead(JOYSTICK_Y);

  //Filtra usando la ventana
  int xMeanValue = meanFilter.AddValue(xValue);
  int yMeanValue = meanFilter.AddValue(yValue);

  //Aplicar suavizado
  xValueFiltered += smoothFactor * (xMeanValue - xValueFiltered);
  yValueFiltered += smoothFactor * (yMeanValue - yValueFiltered);

  //Mapear los valores en los ejex X y Y
  float xValueMap = (xValueFiltered - 1935)/1935.0;
  float yValueMap = (yValueFiltered - 1935)/1935.0;

  Serial.flush();

  Serial.print(xValueMap);
  Serial.print("|");
  Serial.println(yValueMap);

  if(Serial.available() > 0){
    receivedChar = Serial.read();
    if(receivedChar == 1){
      digitalWrite(2,HIGH); //Enciende el LED
    }
    else if(receivedChar == 0){
      digitalWrite(2,LOW); //Apaga el LED
    }
  }
  delay(50);

}
