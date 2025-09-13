/*
 * Servo.cpp
 *
 *  Created on: May 21, 2022
 *      Author: NH HoangXuan
 */

#include <Servo.h>

volatile uint8_t servoLevel = 0;

void vServoInit(Servo *servo, TIM_HandleTypeDef *htim, uint32_t channel,
		osEventFlagsId_t event, uint32_t flag) {
	servo->htim = htim;
	servo->channel = channel;
	switch (channel) {
	case TIM_CHANNEL_1:
		servo->CCRx = &htim->Instance->CCR1;
		break;
	case TIM_CHANNEL_2:
		servo->CCRx = &htim->Instance->CCR2;
		break;
	case TIM_CHANNEL_3:
		servo->CCRx = &htim->Instance->CCR3;
		break;
	case TIM_CHANNEL_4:
		servo->CCRx = &htim->Instance->CCR4;
		break;
	}
	//
	servo->event = event;
	servo->flag = flag;
}
void vServoStart(Servo *servo) {
	HAL_TIM_PWM_Start(servo->htim, servo->channel);
	if (servo->isReverse)
		*servo->CCRx = servo->downLimit;
	else
		*servo->CCRx = servo->upLimit;
}
void vServoStop(Servo *servo) {
	HAL_TIM_PWM_Stop(servo->htim, servo->channel);
}

void vServoMove1(Servo *servo, eServoCommand cmd) {
	servo->command = cmd;
	vServoReverseCommand(servo);
	if (servo->command != eServoStop)
		osEventFlagsSet(servo->event, servo->flag);
}

void vServoStop1(Servo *servo) {
	osEventFlagsClear(servo->event, servo->flag);
	servo->command = eServoStop;
}

eServoCommand eServoMove(Servo *servo) {
	osEventFlagsWait(servo->event, servo->flag, osFlagsWaitAll | osFlagsNoClear,
	osWaitForever);

	eServoCommand res = eServoStop;
	uint8_t step = servo->step;
	uint8_t downLimit = servo->downLimit;
	uint8_t upLimit = servo->upLimit;
	uint8_t value = *servo->CCRx;

	switch (servo->command) {
	case eServoStop:
		osEventFlagsClear(servo->event, servo->flag);
		break;
	case eServoUp1:
		if (*servo->CCRx + step <= upLimit) {
			*servo->CCRx += step;
			if (value < downLimit)
				*servo->CCRx = downLimit;
			else if (value > upLimit)
				*servo->CCRx = upLimit;
		} else {
			*servo->CCRx = upLimit;
			servo->command = eServoStop;
			res = eServoUp1;
		}
		osEventFlagsClear(servo->event, servo->flag);
		break;
	case eServoDown1:
		if (*servo->CCRx - step >= downLimit) {
			*servo->CCRx -= step;
			if (value < downLimit)
				*servo->CCRx = downLimit;
			else if (value > upLimit)
				*servo->CCRx = upLimit;
		} else {
			*servo->CCRx = downLimit;
			servo->command = eServoStop;
			res = eServoDown1;
		}
		osEventFlagsClear(servo->event, servo->flag);
		break;
	case eServoUp:
		if (*servo->CCRx + step <= upLimit) {
			*servo->CCRx += step;
			if (value < downLimit)
				*servo->CCRx = downLimit;
			else if (value > upLimit)
				*servo->CCRx = upLimit;
		} else {
			*servo->CCRx = upLimit;
			servo->command = eServoStop;
			res = eServoUp;
		}
		break;
	case eServoDown:
		if (*servo->CCRx - step >= downLimit) {
			*servo->CCRx -= step;
			if (value < downLimit)
				*servo->CCRx = downLimit;
			else if (value > upLimit)
				*servo->CCRx = upLimit;
		} else {
			*servo->CCRx = downLimit;
			servo->command = eServoStop;
			res = eServoDown;
		}
		break;
	}

	osDelay(servo->delay);

	return res;
}

void vServoReverseCommand(Servo *servo) {
	if (servo->isReverse) {
		switch (servo->command) {
		case eServoStop:
			break;
		case eServoUp1:
			servo->command = eServoDown1;
			break;
		case eServoDown1:
			servo->command = eServoUp1;
			break;
		case eServoUp:
			servo->command = eServoDown;
			break;
		case eServoDown:
			servo->command = eServoUp;
			break;
		}
	}
}

void vServoUpdateCommand(Servo *servo, uint8_t *rxData, uint8_t offset) {
	osEventFlagsClear(servo->event, servo->flag);

	servo->step = rxData[offset];
	servo->delay = rxData[offset + 1];
	servo->command = (eServoCommand) rxData[offset + 2];

//	if (servo->isReverse) {
//		switch (servo->command) {
//		case eServoStop:
//			break;
//		case eServoUp1:
//			servo->command = eServoDown1;
//			break;
//		case eServoDown1:
//			servo->command = eServoUp1;
//			break;
//		case eServoUp:
//			servo->command = eServoDown;
//			break;
//		case eServoDown:
//			servo->command = eServoUp;
//			break;
//		}
//	}

	vServoReverseCommand(servo);

	if (servo->command != eServoStop)
		osEventFlagsSet(servo->event, servo->flag);
}

void vServoUpdateSetup(Servo *servo, uint8_t *rxData, uint8_t offset) {
	osEventFlagsClear(servo->event, servo->flag);

	servo->command = eServoStop;

	servo->downLimit = rxData[offset];
	servo->upLimit = rxData[offset + 1];
	servo->isReverse = rxData[offset + 2];
}
