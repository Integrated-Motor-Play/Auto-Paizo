#define Pin1 10
#define Pin2 11
#define Pin3 12

char data = 0;                //Variable for storing received data
void setup()
{
  Serial.begin(9600);
  pinMode(Pin1, OUTPUT);
  pinMode(Pin2, OUTPUT);
  pinMode(Pin3, OUTPUT);
  digitalWrite(Pin1, HIGH);
  digitalWrite(Pin2, HIGH);
  digitalWrite(Pin3, HIGH);
}
void loop()
{
  if (Serial.available() > 0) // Send data only when you receive data:
  {
    data = Serial.read();      //Read the incoming data and store it into variable data
    Serial.print(data);
    Serial.print("\n");
    if (data == 'A')
      digitalWrite(Pin1, LOW);
    else if (data == 'a')
      digitalWrite(Pin1, HIGH);
    else if (data == 'B')
      digitalWrite(Pin2, LOW);
    else if (data == 'b')
      digitalWrite(Pin2, HIGH);
    else if (data == 'C')
      digitalWrite(Pin3, LOW);
    else if (data == 'c')
      digitalWrite(Pin3, HIGH);
  }
}
