/* This script is made to read INPUTS from potentiometers and WRITE the current value.
Said value will also be used sent through Unity to alter a model, then used as an
OUTPUT for the LEDs to represent the changes made to the digital model within Unity. */


// Constants created for ports.

// A# = analog ports for potentiometer.
const pin_size_t pot1Pin = A0;
const pin_size_t pot2Pin = A1;
const pin_size_t pot3Pin = A2;
const pin_size_t pot4Pin = A3;

// ~# PWM ports for LEDs.
const pin_size_t led1Pin = 11;
const pin_size_t led2Pin = 10;
const pin_size_t led3Pin = 9;
const pin_size_t led4Pin = 6;

// Button port.
const pin_size_t button = 13;

// Delaytime for how big the interval between each print should be.
const uint8_t delayTime = 100;
bool hasButtonBeenPressed = false;

uint16_t potValues[4];
uint16_t ledValues[4];

void setupInput(pin_size_t pin);
void setupOutput(pin_size_t pin);
int mapValue(int value);
bool buttonPress(PinStatus status);

void setup() {
  // put your setup code here, to run once.

  // Is the value 9600 in like 90% of the time. Idk what it means.
  Serial.begin(9600);

  setupInput(pot1Pin);
  setupInput(pot2Pin);
  setupInput(pot3Pin);
  setupInput(pot4Pin);
  setupInput(button);

  setupOutput(led1Pin);
  setupOutput(led2Pin);
  setupOutput(led3Pin);
  setupOutput(led4Pin);
}

void loop() {
  // put your main code here, to run repeatedly: <-- I didn't write this

  // Value of the potentiometers are read.
  potValues[0] = analogRead(pot1Pin);
  potValues[1] = analogRead(pot2Pin);
  potValues[2] = analogRead(pot3Pin);
  potValues[3] = analogRead(pot4Pin);

  // Read potentiometer values are printed and how it is sendt to Unity.
  Serial.print(potValues[0]);
  Serial.print(',');
  Serial.print(potValues[1]);
  Serial.print(',');
  Serial.print(potValues[2]);
  Serial.print(',');
  Serial.print(potValues[3]);
  Serial.print(',');
  Serial.println(buttonPress(digitalRead(button)));

  // Here we delay the loop by the delayTime value specified.
  delay(delayTime);

  // LEDs use potentiometer value for brightness.
  analogWrite(led1Pin, mapValue(potValues[0]));
  analogWrite(led2Pin, mapValue(potValues[1]));
  analogWrite(led3Pin, mapValue(potValues[2]));
  analogWrite(led4Pin, mapValue(potValues[3]));
}



// pinMode swaps the pins between INPUT and OUTPUT mode.
void setupInput(pin_size_t pin)
{
  pinMode(pin, INPUT);
}

void setupOutput(pin_size_t pin)
{
  pinMode(pin, OUTPUT);
}

// Potentiometer range changed to 0-255.
// Each LED will represent one colour from the RGBA colour values.
int mapValue(int value)
{
  return map(value, 0, 1023, 0, 255);
}

bool buttonPress(PinStatus status)
{
  if(status == LOW) return true;
  return false; 
}
