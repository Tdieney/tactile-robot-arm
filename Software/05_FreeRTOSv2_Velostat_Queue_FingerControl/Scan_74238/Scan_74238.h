/*
 * Scan_74238.h
 *
 *  Created on: May 15, 2022
 *      Author: NH HoangXuan
 */

#ifndef SCAN_74238_H_
#define SCAN_74238_H_

#include "main.h"
#include "stm32f1xx_hal.h"

#ifndef NumberOfScanLimit
#define NumberOfScanLimit	(44u)
#endif

#define A_Port	A_Scan_GPIO_Port
#define B_Port 	B_Scan_GPIO_Port
#define C_Port 	C_Scan_GPIO_Port
#define D_Port 	D_Scan_GPIO_Port
#define E_Port 	E_Scan_GPIO_Port
#define F_Port 	F_Scan_GPIO_Port

#define A_Pin	A_Scan_Pin
#define B_Pin	B_Scan_Pin
#define C_Pin	C_Scan_Pin
#define D_Pin	D_Scan_Pin
#define E_Pin	E_Scan_Pin
#define F_Pin	F_Scan_Pin

//extern uint16_t ScanRowth;

void Scan_74238(uint16_t scanRowth);

#endif /* SCAN_74238_H_ */
