const int leftFSRPin = A1;
const int rightFSRPin = A0;

int leftFsrValue = 0;  
int rightFsrValue = 0;
int lastLeftValue = 0;
int lastRightValue = 0;

bool first = true;

const float THRESHOLD = 10;

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
    int output = 0;
  
    if(rightFsrValue > lastRightValue + THRESHOLD || rightFsrValue < lastRightValue - THRESHOLD)
    {
      output += 2;
    }
    
    if(leftFsrValue > lastLeftValue + THRESHOLD || leftFsrValue < lastLeftValue - THRESHOLD)
    {
      output += 1;
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

