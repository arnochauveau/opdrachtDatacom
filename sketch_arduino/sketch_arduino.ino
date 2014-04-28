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

#include <LiquidCrystal.h>
int buttonpin = 12; //kies de pin voor de BUTTON
int val = 0; //variable voor status buttonpin

int lcdRS = 10;
int lcdRW = 9;

int lcdEN = 8;

int lcdd4 =7;
int lcdd5 = 6;
int lcdd6 = 5;
int lcdd7 = 4;

LiquidCrystal lcd(lcdRS,lcdRW,lcdEN,lcdd4,lcdd5,lcdd6,lcdd7);

void setup()
{
  
  lcd.begin(16,2);
  
  pinMode(buttonpin, INPUT_PULLUP); //declareren van de buttonpin als input
  lcd.clear();
  
 
  
  Serial.begin(9600);
  
  //Sendval(0,0,1,0,0,1,0);
  //Sendval(0,0,1,1,0,0,0);
  
  
}

void loop()
{
  val = digitalRead(buttonpin); //value van buttonpin in var steken
  // Serial.print(val);
  if(val == LOW){
    Serial.print("buttonpress");
    buttonpressed();
  }
}
void buttonpressed(){
lcd.clear();
lcd.print("Background check");
lcd.setCursor(0,1);
lcd.print("By NSA busy...");
}







