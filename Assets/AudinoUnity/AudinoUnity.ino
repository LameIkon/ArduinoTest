/* This script is made to read INPUTS from potentiometers and WRITE the current value.
Said value will also be used sent through Unity to alter a model, then used as an
OUTPUT for the LEDs to represent the changes made to the digital model within Unity. */


// Constants created for ports.

// A# = analog ports for potentiometer.
const int pot1Pin = A0;
const int pot2Pin = A1;
const int pot3Pin = A2;
const int pot4Pin = A3;

// ~# PWM ports for LEDs.
const int led1Pin = 11;
const int led2Pin = 10;
const int led3Pin = 9;
const int led4Pin = 6;

// Button port.
const int button = 13;

// Delaytime for how big the interval between each print should be.
const int delayTime = 100;

int potValues[4];
int ledValues[4];

void setupInput(pin_size_t pin);
void setupOutput(pin_size_t pin);
int mapValue(int value);

void setup() {
  // put your setup code here, to run once.

  // Is the value 9600 in like 90% of the time. Idk what it means.
  Serial.begin(9600);

  setupInput(pot1Pin);
  setupInput(pot2Pin);
  setupInput(pot3Pin);
  setupInput(pot4Pin);

  setupOutput(led1Pin);
  setupOutput(led2Pin);
  setupOutput(led3Pin);
  setupOutput(led4Pin);
}

void loop() {
  // put your main code here, to run repeatedly: <-- I didn't write this

  // Value of the potentiometers are read.
  potValues[0] = mapValue(analogRead(pot1Pin));
  potValues[1] = mapValue(analogRead(pot2Pin));
  potValues[2] = mapValue(analogRead(pot3Pin));
  potValues[3] = mapValue(analogRead(pot4Pin));

  // Read potentiometer values are printed.
  Serial.print(potValues[0]);
  Serial.print(',');
  Serial.print(potValues[1]);
  Serial.print(',');
  Serial.print(potValues[2]);
  Serial.print(',');
  Serial.print(potValues[3]);

  // Here we delay the loop by the delayTime value specified.
  delay(delayTime);

  // LEDs use potentiometer value for brightness.
  analogWrite(led1Pin, potValues[0]);
  analogWrite(led2Pin, potValues[1]);
  analogWrite(led3Pin, potValues[2]);
  analogWrite(led4Pin, potValues[3]);
  
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
  return map(value, 0, 1023, 255, 0);
}
