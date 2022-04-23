#include <SoftwareSerial.h>

#define RX  18
#define TX  19
#define LED 33


SoftwareSerial mySerial(RX, TX);


char data;

void setup() {
  pinMode(LED, OUTPUT);
  Serial.begin(9600);
  mySerial.begin(9600);
  data = 0;
}

void loop()
{
  if (Serial.available())
  {
    //Serial.println(Serial.available());
    // data received from UNITY
    data = Serial.read();

    switch (data)
    {
      case 'Y':
        digitalWrite(LED, HIGH);
        Serial.print('1'); //send feedback to unity
        break;
      case 'N':
        digitalWrite(LED, LOW);
        Serial.print('0');  //send feedback to unity
        break;
    }
  }
}
