const int leftFSRPin = A1;
const int rightFSRPin = A0;

int leftFsrValue = 0;  
int rightFsrValue = 0;

#define NUM_FRAMES 10
const float THRESHOLD = 20;

struct Frame
{
  float value;
  int life;
};

float framesRight[NUM_FRAMES];
float framesLeft[NUM_FRAMES];

float leftAvg = 0;
float rightAvg = 0;

int currentIndexRight = 0;
int currentIndexLeft = 0;

bool leftStep = true;
bool first = true;

int freeRam () 
{
  extern int __heap_start, *__brkval; 
  int v; 
  return (int) &v - (__brkval == 0 ? (int) &__heap_start : (int) __brkval); 
}

void setup() 
{
  Serial.begin(9600);
  leftAvg = 0;
  rightAvg = 0;
}

void loop() 
{
  leftFsrValue = analogRead(leftFSRPin);
  rightFsrValue = analogRead(rightFSRPin);

  int output = 3; // base2 = 11, both legs down

  if(first)
  {
    for(int i = 0; i < NUM_FRAMES; i++)
    {
      framesLeft[i] = leftFsrValue;
      framesRight[i] = rightFsrValue;
    }
    leftAvg = 0;
    rightAvg = 0;
    Serial.println(String(leftFsrValue) + "," + String(rightFsrValue) + " : " + String(leftAvg) + "," + String(rightAvg));
    first = false;
  }
  else
  {  
    if(rightFsrValue < rightAvg - THRESHOLD)
    {
      // leg up, dont count toward avg
      output--;
    }
    else
    {
      // count toward avg
      currentIndexRight ++;
      if(currentIndexRight >= NUM_FRAMES)
        currentIndexRight = 0;
      framesRight[currentIndexRight] = rightFsrValue;

      float sum = 0;
      for(int i = 0; i < NUM_FRAMES; i++)
      {
        sum += framesRight[i];
      }

      rightAvg = sum / (float)NUM_FRAMES;
    }

    if(leftFsrValue < leftAvg - THRESHOLD)
    {
      // leg up, dont count toward avg
      output -= 2;
    }
    else
    {
      // count toward avg
      currentIndexLeft ++;
      if(currentIndexLeft >= NUM_FRAMES)
        currentIndexLeft = 0;
      framesLeft[currentIndexLeft] = leftFsrValue;

      float sum = 0;
      for(int i = 0; i < NUM_FRAMES; i++)
      {
        sum += framesLeft[i];
      }

      leftAvg = sum / (float)NUM_FRAMES;
    }
    
    //Serial.println(output);
    
    Serial.println(String(leftFsrValue) + "," + String(rightFsrValue) + " : " + String(leftAvg) + "," + String(rightAvg));
    //Serial.println(String(leftFsrValue) + "," + String(rightFsrValue) + " : " + String(leftMid) + "," + String(rightMid));
    //Serial.println(freeRam());
  }

  delay(100);
}

