/*
 * Servo.hpp
 *
 *  Created on: May 21, 2022
 *      Author: NH HoangXuan
 */

#ifndef INC_SERVO_H_
#define INC_SERVO_H_

#include "stm32f1xx_hal.h"
#include "cmsis_os.h"

#define Head			0xAAu
#define Tail			0x55u

#ifndef NumberOfRxData
#define NumberOfRxData	21u
#endif

extern volatile uint8_t servoLevel;

typedef enum {
	eServoStop, eServoUp1, eServoDown1, eServoUp, eServoDown
} eServoCommand;

typedef struct {
	//command
	uint8_t step;
	uint8_t delay;
	eServoCommand command;
	//setup
	uint8_t downLimit;
	uint8_t upLimit;
	uint8_t isReverse;
	//
	TIM_HandleTypeDef *htim;
	uint32_t channel;
	__IO uint32_t *CCRx;
	//RTOS
	osEventFlagsId_t event;
	uint32_t flag;
} Servo;

void vServoInit(Servo *servo, TIM_HandleTypeDef *htim, uint32_t channel,
		osEventFlagsId_t event, uint32_t flag);
void vServoStart(Servo *servo);
void vServoStop(Servo *servo);
eServoCommand eServoMove(Servo *servo);
void vServoReverseCommand(Servo *servo);
void vServoUpdateCommand(Servo *servo, uint8_t *rxData, uint8_t offset);
void vServoUpdateSetup(Servo *servo, uint8_t *rxData, uint8_t offset);
//
void vServoMove1(Servo *servo, eServoCommand cmd);
void vServoStop1(Servo *servo);

#endif /* INC_SERVO_H_ */
