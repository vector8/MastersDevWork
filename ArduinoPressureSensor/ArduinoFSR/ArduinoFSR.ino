#include <LinkedList.h>
#include <QueueArray.h>

const int leftFSRPin = A0;
const int rightFSRPin = A1;

int leftFsrValue = 0;  
int rightFsrValue = 0;

const float NUM_FRAMES = 50;
const float THRESHOLD = 10;

float sumRight;
float sumLeft;

QueueArray<float> framesRight;
QueueArray<float> framesLeft;

bool leftStep = true;

float prevRunningAvgRight;
float prevRunningAvgLeft;

void setup() 
{
  Serial.begin(9600);

  for(int i = 0; i < NUM_FRAMES; i++)
  {
    framesRight.push(0);
    framesLeft.push(0);
  }

  sumRight = 0;
  sumLeft = 0;
  prevRunningAvgRight = 0;
  prevRunningAvgLeft = 0;
}

void loop() 
{
  leftFsrValue = analogRead(leftFSRPin);
  rightFsrValue = analogRead(rightFSRPin);

  float poppedRight = framesRight.pop();
  float poppedLeft = framesLeft.pop();
  framesRight.push(rightFsrValue);
  framesLeft.push(leftFsrValue);

  sumRight = (sumRight - poppedRight) + rightFsrValue;
  sumLeft = (sumLeft - poppedLeft) + leftFsrValue;

  float runningAvgRight = sumRight / NUM_FRAMES;
  float runningAvgLeft = sumLeft / NUM_FRAMES;

  if(rightFsrValue <= prevRunningAvgRight - THRESHOLD)
  {
    Serial.println("Right leg up");
  }
  
  if(leftFsrValue <= prevRunningAvgLeft - THRESHOLD)
  {
    Serial.println("Left leg up");
  }
//  if(leftStep && (leftFsrValue - rightFsrValue) > 100)
//  {
//    leftStep = false;
//    Serial.println("Left step!");
//  }
//  else if(!leftStep && (rightFsrValue - leftFsrValue) > 100)
//  {
//    leftStep = true;
//    Serial.println("Right step!");
//  }

  String leftString = String(leftFsrValue);
  String rightString = String(rightFsrValue);
  Serial.println(leftString + "," + rightString);

  prevRunningAvgRight = runningAvgRight;
  prevRunningAvgLeft = runningAvgLeft;

  delay(100);
}
