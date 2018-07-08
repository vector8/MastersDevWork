const int leftFSRPin = A1;
const int rightFSRPin = A0;

int leftFsrValue = 0;  
int rightFsrValue = 0;
int lastLeftValue = 0;
int lastRightValue = 0;

bool first = true;
bool leftUp = true;
bool rightUp = true;
int output = 0;

const float THRESHOLD = 25;

int freeRam () {
  extern int __heap_start, *__brkval; 
  int v; 
  return (int) &v - (__brkval == 0 ? (int) &__heap_start : (int) __brkval); 
}

void setup() 
{
  Serial.begin(9600);
}

void loop() 
{
  leftFsrValue = analogRead(leftFSRPin);
  rightFsrValue = analogRead(rightFSRPin);

  if(first)
  {
    lastLeftValue = leftFsrValue;
    lastRightValue = rightFsrValue;
    first = false;
  }
  else
  {  
    if(rightFsrValue < lastRightValue - THRESHOLD)
    {
      rightUp = true;
    }
    else if( rightFsrValue > lastRightValue + THRESHOLD)
    {
      rightUp = false;
    }
    
    if(leftFsrValue < lastLeftValue - THRESHOLD)
    {
      leftUp = true;
    }
    else if(leftFsrValue > lastLeftValue + THRESHOLD)
    {
      leftUp = false;
    }

    if(leftUp && rightUp)
    {
      output = 0;
    }
    else
    {
      output = 1;
    }

     Serial.println(String(output));
    //String leftString = String(leftFsrValue);
    //String rightString = String(rightFsrValue);
    //Serial.println(leftString + "," + rightString + " : " + String(leftMid) + "," + String(rightMid));
//    Serial.println(freeRam());

    lastLeftValue = leftFsrValue;
    lastRightValue = rightFsrValue;
  }

  delay(100);
}

