/*
 * Scan_74238.c
 *
 *  Created on: May 15, 2022
 *      Author: NH HoangXuan
 */

#include "main.h"
#include "Scan_74238.h"

//uint16_t ScanRowth = 0;

GPIO_PinState ScanBitState0;
GPIO_PinState ScanBitState1;
GPIO_PinState ScanBitState2;
GPIO_PinState ScanBitState3;
GPIO_PinState ScanBitState4;
GPIO_PinState ScanBitState5;

//void Scan_74238() {
//	ScanBitState0 = (ScanRowth & (1 << 0)) ? GPIO_PIN_SET : GPIO_PIN_RESET;
//	ScanBitState1 = (ScanRowth & (1 << 1)) ? GPIO_PIN_SET : GPIO_PIN_RESET;
//	ScanBitState2 = (ScanRowth & (1 << 2)) ? GPIO_PIN_SET : GPIO_PIN_RESET;
//	ScanBitState3 = (ScanRowth & (1 << 3)) ? GPIO_PIN_SET : GPIO_PIN_RESET;
//	ScanBitState4 = (ScanRowth & (1 << 4)) ? GPIO_PIN_SET : GPIO_PIN_RESET;
//	ScanBitState5 = (ScanRowth & (1 << 5)) ? GPIO_PIN_SET : GPIO_PIN_RESET;
//
//	HAL_GPIO_WritePin(A_Port, A_Pin, ScanBitState0);
//	HAL_GPIO_WritePin(B_Port, B_Pin, ScanBitState1);
//	HAL_GPIO_WritePin(C_Port, C_Pin, ScanBitState2);
//	HAL_GPIO_WritePin(D_Port, D_Pin, ScanBitState3);
//	HAL_GPIO_WritePin(E_Port, E_Pin, ScanBitState4);
//	HAL_GPIO_WritePin(F_Port, F_Pin, ScanBitState5);
//
//	++ScanRowth;
//	ScanRowth %= NumberOfScanLimit; //0 -> 43
//}

void Scan_74238(uint16_t scanRowth) {
	ScanBitState0 = (scanRowth & (1 << 0)) ? GPIO_PIN_SET : GPIO_PIN_RESET;
	ScanBitState1 = (scanRowth & (1 << 1)) ? GPIO_PIN_SET : GPIO_PIN_RESET;
	ScanBitState2 = (scanRowth & (1 << 2)) ? GPIO_PIN_SET : GPIO_PIN_RESET;
	ScanBitState3 = (scanRowth & (1 << 3)) ? GPIO_PIN_SET : GPIO_PIN_RESET;
	ScanBitState4 = (scanRowth & (1 << 4)) ? GPIO_PIN_SET : GPIO_PIN_RESET;
	ScanBitState5 = (scanRowth & (1 << 5)) ? GPIO_PIN_SET : GPIO_PIN_RESET;

	HAL_GPIO_WritePin(A_Port, A_Pin, ScanBitState0);
	HAL_GPIO_WritePin(B_Port, B_Pin, ScanBitState1);
	HAL_GPIO_WritePin(C_Port, C_Pin, ScanBitState2);
	HAL_GPIO_WritePin(D_Port, D_Pin, ScanBitState3);
	HAL_GPIO_WritePin(E_Port, E_Pin, ScanBitState4);
	HAL_GPIO_WritePin(F_Port, F_Pin, ScanBitState5);
}
