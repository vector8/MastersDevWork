const int leftFSRPin = A1;
const int rightFSRPin = A0;

int leftFsrValue = 0;  
int rightFsrValue = 0;

#define NUM_FRAMES 10
const float THRESHOLD = 10;

struct Frame
{
  float value;
  int life;
};

float framesRight[NUM_FRAMES];
float framesLeft[NUM_FRAMES];

int currentIndex = 0;

bool leftStep = true;
bool first = true;

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
    memset(framesRight, rightFsrValue, NUM_FRAMES);
    memset(framesLeft, leftFsrValue, NUM_FRAMES);
    first = false;
  }
  else
  {  
    currentIndex ++;
    if(currentIndex >= NUM_FRAMES)
      currentIndex = 0;
    framesRight[currentIndex] = rightFsrValue;
    framesLeft[currentIndex] = leftFsrValue;
  
    float rightMax = 0;
    float rightMin = 1024;
    float leftMax = 0;
    float leftMin = 1024;
  
    for(int i = 0; i < NUM_FRAMES; i++)
    {
      if(framesRight[i] > rightMax)
        rightMax = framesRight[i];
      if(framesRight[i] < rightMin)
        rightMin = framesRight[i];
  
      if(framesLeft[i] > leftMax)
        leftMax = framesLeft[i];
      if(framesLeft[i] < leftMin)
        leftMin = framesLeft[i];
    }
  
    float rightMid = (rightMin + (3.0f * rightMax)) / 4.0f;
    float leftMid = (leftMin + (3.0f * leftMax)) / 4.0f;

    int rightLeg = 0, leftLeg = 0;
  
    if(rightFsrValue > rightMin + THRESHOLD)
    {
      rightLeg = 1;
    }
    
    if(leftFsrValue > leftMin + THRESHOLD)
    {
      leftLeg = 1;
    }

    Serial.println(String(leftLeg) + String(rightLeg));
  
    //String leftString = String(leftFsrValue);
    //String rightString = String(rightFsrValue);
    //Serial.println(leftString + "," + rightString + " : " + String(leftMid) + "," + String(rightMid));
//    Serial.println(freeRam());
  }

  delay(100);
}

