// arduino      // LCD
// GND             // 1
// +5v             // 2
// RS = 10         // 4            
// RW = 9        // 5
// ENABLE = 8     // 6
// d4 = 7          // 11
// d5 = 6          // 12
// d6 = 5          // 13
// d7 = 4          // 14

int buttonpin = 12; //kies de pin voor de BUTTON
int val = 0; //variable voor status buttonpin

void setup()
{
  pinMode(buttonpin, INPUT_PULLUP); //declareren van de buttonpin als input
  Serial.begin(9600);
}

void loop()
{
  val = digitalRead(buttonpin); //value van buttonpin in var steken
  // Serial.print(val);
  if(val == LOW){
    Serial.print("buttonpress");
  
  }
}

