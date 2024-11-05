#include <AccelStepper.h>
#include <String.h>

bool onState = false;
int motorPos = 0;
int motorToGo = 0;

AccelStepper stepper; // Defaults to AccelStepper::FULL4WIRE (4 pins) on 2, 3, 4, 5

int positions[5] = {105, 310, 515, 720, 925};
int brojPoljaInt=0;
int kodPoljaInt=0;
String brojPoljaStr="";
String kodPoljaStr="";

void setup()
{
  stepper.setMaxSpeed(500);
  stepper.setSpeed(300);
  stepper.setAcceleration(500);
  Serial.begin(9600);
  Serial.setTimeout(50);
}

void loop()
{
  dataRead();
}

String data = "";
void dataRead() {

  if (Serial.available())
  {
    data= Serial.readStringUntil('/');

    if(data.substring(0,1).equals("@")){
     brojPoljaStr = data.substring(1,data.length());
     brojPoljaInt = brojPoljaStr.toInt();
     
      }
    else if (data.substring(0,1).equals("#")) {
      kodPoljaStr = data.substring(1,data.length());
      kodPoljaInt = kodPoljaStr.toInt();
      myStepper(2048*kodPoljaInt/brojPoljaInt);
     }
    
    data = "";
    Serial.flush();
  }
}

void myStepper(int pos) {

  stepper.moveTo(pos);
  while (stepper.run() != 0) {
  }
  
}
