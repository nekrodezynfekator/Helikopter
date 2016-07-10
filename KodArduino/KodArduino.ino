int pulseLength = 0;
byte dane[4];

void setup() {
  Serial.begin(9600);
  pinMode(11, OUTPUT);
  dane[0] = byte(63);
  dane[1] = byte(63);
  dane[2] = byte(0);
  dane[3] = byte(63);
}

void loop() {
  if (Serial.available() > 0) {
    int naglowekOdebrany = Serial.read();
    switch (naglowekOdebrany) {
      case (0): dane[0] = Serial.read(); break; //yaw
      case (1): dane[1] = Serial.read(); break; //pitch
      case (2): dane[2] = Serial.read(); break; //throttle
      case (3): dane[3] = Serial.read(); break; //trim
      default: break;
    }
  }

  //for (int i = 0; i < 5; i++)
  sekwencja(dane[0], dane[1], dane[2], dane[3]);
  digitalWrite(11, LOW);
  delay(100);
}
void pulsy(long microsecs) {
  cli();
  while (microsecs > 0) {
    digitalWrite(11, HIGH);
    delayMicroseconds(10);
    digitalWrite(11, LOW);
    delayMicroseconds(10);
    microsecs -= 26;
  }
  sei();
}

void zer()
{
  pulsy(256);
  delayMicroseconds(341);
  pulseLength += 597;
}

void jed()
{
  pulsy(277);
  delayMicroseconds(725);
  pulseLength += 1002;
}

void sekwencja(byte yaw, byte pitch, byte throttle, byte trim) {
  int potegi[8] = {128, 64, 32, 16, 8, 4, 2, 1};
  int data[4];
  data[0] = yaw;
  data[1] = pitch;
  data[2] = throttle;
  data[3] = trim;

  pulsy(1941);
  delayMicroseconds(2048);
  pulseLength = 3989;
  for (int j = 0; j < 4; j++) {
    for (int i = 0; i < 8; i++) {
      if (data[j] - potegi[i] >= 0) {
        jed();
        data[j] = data[j] - potegi[i];
      }
      else {
        zer();
      }
    }
  }
  pulsy(277);
  delayMicroseconds( (122500 - pulseLength) ); //(122500 - pulseLength)
}
