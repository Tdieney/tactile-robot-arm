/* USER CODE BEGIN Header */
/**
  ******************************************************************************
  * @file           : main.h
  * @brief          : Header for main.c file.
  *                   This file contains the common defines of the application.
  ******************************************************************************
  * @attention
  *
  * <h2><center>&copy; Copyright (c) 2022 STMicroelectronics.
  * All rights reserved.</center></h2>
  *
  * This software component is licensed by ST under Ultimate Liberty license
  * SLA0044, the "License"; You may not use this file except in compliance with
  * the License. You may obtain a copy of the License at:
  *                             www.st.com/SLA0044
  *
  ******************************************************************************
  */
/* USER CODE END Header */

/* Define to prevent recursive inclusion -------------------------------------*/
#ifndef __MAIN_H
#define __MAIN_H

#ifdef __cplusplus
extern "C" {
#endif

/* Includes ------------------------------------------------------------------*/
#include "stm32f1xx_hal.h"

/* Private includes ----------------------------------------------------------*/
/* USER CODE BEGIN Includes */

/* USER CODE END Includes */

/* Exported types ------------------------------------------------------------*/
/* USER CODE BEGIN ET */

/* USER CODE END ET */

/* Exported constants --------------------------------------------------------*/
/* USER CODE BEGIN EC */

/* USER CODE END EC */

/* Exported macro ------------------------------------------------------------*/
/* USER CODE BEGIN EM */

/* USER CODE END EM */

void HAL_TIM_MspPostInit(TIM_HandleTypeDef *htim);

/* Exported functions prototypes ---------------------------------------------*/
void Error_Handler(void);

/* USER CODE BEGIN EFP */

/* USER CODE END EFP */

/* Private defines -----------------------------------------------------------*/
#define FlagVelostatADC_ConvCplt (1u << 0)
#define FlagVelostatDMAChn2_XferCplt (1u << 1)
#define FlagVelostatUART_TxCplt (1u << 2)
#define FlagVelostatSortDataNotFull (1u << 3)
#define FlagVelostatSortDataFull (1u << 4)
#define NumberOfColums 10u
#define NumberOfRows 44u
#define NumberOfConversion 5u
#define NumberOfADCChannels 10u
#define NumberOfCells (NumberOfColums * NumberOfRows)
#define TxBufferSize (NumberOfCells + 2u)
#define NumberOfScanLimit 44u
#define FlagHandUART_RxCplt (1u << 0)
#define FlagHandBot1 (1u << 1)
#define FlagHandTop1 (1u << 2)
#define FlagHandBot2 (1u << 3)
#define FlagHandTop2 (1u << 4)
#define FlagHandBot3 (1u << 5)
#define FlagHandTop3 (1u << 6)
#define FlagVelostatEnqueue (1u << 6)
#define FlagVelostatDMAChn3_XferCplt (1u << 5)
#define FlagVelostatEnable (1u << 7)
#define FlagHandPeakControlEnable (1u << 7)
#define FlagHandDMAChn6_XferCplt (1u << 8)
#define FlagHandMatrix (1u << 9)
#define LEDGreen_Pin GPIO_PIN_13
#define LEDGreen_GPIO_Port GPIOC
#define Btn0_Pin GPIO_PIN_14
#define Btn0_GPIO_Port GPIOC
#define Btn0_EXTI_IRQn EXTI15_10_IRQn
#define Btn1_Pin GPIO_PIN_15
#define Btn1_GPIO_Port GPIOC
#define Btn1_EXTI_IRQn EXTI15_10_IRQn
#define A_Scan_Pin GPIO_PIN_2
#define A_Scan_GPIO_Port GPIOB
#define B_Scan_Pin GPIO_PIN_12
#define B_Scan_GPIO_Port GPIOB
#define C_Scan_Pin GPIO_PIN_13
#define C_Scan_GPIO_Port GPIOB
#define D_Scan_Pin GPIO_PIN_14
#define D_Scan_GPIO_Port GPIOB
#define E_Scan_Pin GPIO_PIN_15
#define E_Scan_GPIO_Port GPIOB
#define F_Scan_Pin GPIO_PIN_8
#define F_Scan_GPIO_Port GPIOA
/* USER CODE BEGIN Private defines */
#define FlagVelostatADC_ConvCplt 			(1u << 0)
#define FlagVelostatDMAChn2_XferCplt 		(1u << 1)
#define FlagVelostatUART_TxCplt 			(1u << 2)
#define FlagVelostatSortDataNotFull 		(1u << 3)
#define FlagVelostatSortDataFull 			(1u << 4)
#define FlagVelostatDMAChn3_XferCplt 		(1u << 5)
#define FlagVelostatEnqueue 				(1u << 6)
#define FlagVelostatEnable 					(1u << 7)

#define FlagHandUART_RxCplt 				(1u << 0)
#define FlagHandBot1 						(1u << 1)
#define FlagHandTop1 						(1u << 2)
#define FlagHandBot2 						(1u << 3)
#define FlagHandTop2 						(1u << 4)
#define FlagHandBot3 						(1u << 5)
#define FlagHandTop3 						(1u << 6)
#define FlagHandPeakControlEnable 			(1u << 7)
#define FlagHandMatrix 						(1u << 8)

#define FlagStopServoBot1					(1u << 1)
#define FlagStopServoBot2					(1u << 2)
#define FlagStopServoBot3					(1u << 3)
#define FlagStopServoTop1					(1u << 4)
#define FlagStopServoTop2					(1u << 5)
#define FlagStopServoTop3					(1u << 6)
/* USER CODE END Private defines */

#ifdef __cplusplus
}
#endif

#endif /* __MAIN_H */

/************************ (C) COPYRIGHT STMicroelectronics *****END OF FILE****/
