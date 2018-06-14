const int leftFSRPin = A1;
const int rightFSRPin = A0;

int leftFsrValue = 0;  
int rightFsrValue = 0;

#define NUM_FRAMES 10
const float THRESHOLD = 5;

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

//void insertValue(float value, LinkedList<Frame> &frames)
//{
//  bool inserted = false;
//  Frame newFrame;
//  newFrame.value = value;
//  newFrame.life = NUM_FRAMES;
//  for(int i = 0; i < frames.size(); i++)
//  {
//    Frame f = frames.get(i);
//
//    f.life--;
//
//    if(f.life <= 0)
//    {
//      frames.remove(i);
//      i--;
//      continue;
//    }
//
//    if(f.value >= value)
//    {
//      frames.add(i, newFrame);
//      inserted = true;
//      break;
//    }
//  }
//  if(!inserted)
//  {
//   frames.add(newFrame);
//  }
//}

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
  //  insertValue(rightFsrValue, framesSortedRight);
  //  insertValue(leftFsrValue, framesSortedLeft);
  
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

    int rightLeg = 1, leftLeg = 1;
  
    if(rightFsrValue <= rightMax - THRESHOLD)
    {
      //Serial.println("Right leg up");
      rightLeg = 0;
    }
    
    if(leftFsrValue <= leftMax - THRESHOLD)
    {
      //Serial.println("Left leg up");
      leftLeg = 0;
    }

    Serial.println(String(rightLeg) + String(leftLeg));
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
  
    //String leftString = String(leftFsrValue);
    //String rightString = String(rightFsrValue);
    //Serial.println(leftString + "," + rightString + " : " + String(leftMid) + "," + String(rightMid));
//    Serial.println(freeRam());
  }

  delay(100);
}

