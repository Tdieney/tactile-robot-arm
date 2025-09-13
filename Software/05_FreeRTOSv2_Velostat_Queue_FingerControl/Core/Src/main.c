/* USER CODE BEGIN Header */
/**
 ******************************************************************************
 * @file           : main.c
 * @brief          : Main program body
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
/* Includes ------------------------------------------------------------------*/
#include "main.h"
#include "cmsis_os.h"

/* Private includes ----------------------------------------------------------*/
/* USER CODE BEGIN Includes */
//#include "usbd_cdc_if.h"
#include <string.h>
#include "Scan_74238.h"
#include "Servo.h"
#include "MyArray.h"
/* USER CODE END Includes */

/* Private typedef -----------------------------------------------------------*/
/* USER CODE BEGIN PTD */

/* USER CODE END PTD */

/* Private define ------------------------------------------------------------*/
/* USER CODE BEGIN PD */
/* USER CODE END PD */

/* Private macro -------------------------------------------------------------*/
/* USER CODE BEGIN PM */

/* USER CODE END PM */

/* Private variables ---------------------------------------------------------*/
ADC_HandleTypeDef hadc1;
ADC_HandleTypeDef hadc2;
DMA_HandleTypeDef hdma_adc1;

TIM_HandleTypeDef htim2;
TIM_HandleTypeDef htim3;
TIM_HandleTypeDef htim4;

UART_HandleTypeDef huart1;
DMA_HandleTypeDef hdma_usart1_tx;
DMA_HandleTypeDef hdma_usart1_rx;

DMA_HandleTypeDef hdma_memtomem_dma1_channel2;
DMA_HandleTypeDef hdma_memtomem_dma1_channel3;
/* Definitions for VelostatScanADC */
osThreadId_t VelostatScanADCHandle;
const osThreadAttr_t VelostatScanADC_attributes = { .name = "VelostatScanADC",
		.stack_size = 128 * 4, .priority = (osPriority_t) osPriorityLow, };
/* Definitions for VelostatProcessOutput */
osThreadId_t VelostatProcessOutputHandle;
const osThreadAttr_t VelostatProcessOutput_attributes = { .name =
		"VelostatProcessOutput", .stack_size = 128 * 4, .priority =
		(osPriority_t) osPriorityLow, };
/* Definitions for VelostatTransmit */
osThreadId_t VelostatTransmitHandle;
const osThreadAttr_t VelostatTransmit_attributes = { .name = "VelostatTransmit",
		.stack_size = 128 * 4, .priority = (osPriority_t) osPriorityLow, };
/* Definitions for HandBot1 */
osThreadId_t HandBot1Handle;
const osThreadAttr_t HandBot1_attributes = { .name = "HandBot1", .stack_size =
		64 * 4, .priority = (osPriority_t) osPriorityLow, };
/* Definitions for HandTop1 */
osThreadId_t HandTop1Handle;
const osThreadAttr_t HandTop1_attributes = { .name = "HandTop1", .stack_size =
		64 * 4, .priority = (osPriority_t) osPriorityLow, };
/* Definitions for HandBot2 */
osThreadId_t HandBot2Handle;
const osThreadAttr_t HandBot2_attributes = { .name = "HandBot2", .stack_size =
		64 * 4, .priority = (osPriority_t) osPriorityLow, };
/* Definitions for HandTop2 */
osThreadId_t HandTop2Handle;
const osThreadAttr_t HandTop2_attributes = { .name = "HandTop2", .stack_size =
		64 * 4, .priority = (osPriority_t) osPriorityLow, };
/* Definitions for HandBot3 */
osThreadId_t HandBot3Handle;
const osThreadAttr_t HandBot3_attributes = { .name = "HandBot3", .stack_size =
		64 * 4, .priority = (osPriority_t) osPriorityLow, };
/* Definitions for HandTop3 */
osThreadId_t HandTop3Handle;
const osThreadAttr_t HandTop3_attributes = { .name = "HandTop3", .stack_size =
		64 * 4, .priority = (osPriority_t) osPriorityLow, };
/* Definitions for HandRxHandler */
osThreadId_t HandRxHandlerHandle;
const osThreadAttr_t HandRxHandler_attributes = { .name = "HandRxHandler",
		.stack_size = 128 * 4, .priority = (osPriority_t) osPriorityHigh, };
/* Definitions for HandMatrixHandler */
osThreadId_t HandMatrixHandlerHandle;
const osThreadAttr_t HandMatrixHandler_attributes = { .name =
		"HandMatrixHandler", .stack_size = 128 * 4, .priority =
		(osPriority_t) osPriorityLow, };
/* Definitions for HandPeakControl */
osThreadId_t HandPeakControlHandle;
const osThreadAttr_t HandPeakControl_attributes = { .name = "HandPeakControl",
		.stack_size = 128 * 4, .priority = (osPriority_t) osPriorityLow, };
/* Definitions for ButtonHandler */
osThreadId_t ButtonHandlerHandle;
const osThreadAttr_t ButtonHandler_attributes = { .name = "ButtonHandler",
		.stack_size = 64 * 4, .priority = (osPriority_t) osPriorityRealtime, };
/* Definitions for HandStopServo */
osThreadId_t HandStopServoHandle;
const osThreadAttr_t HandStopServo_attributes = { .name = "HandStopServo",
		.stack_size = 64 * 4, .priority = (osPriority_t) osPriorityHigh, };
/* Definitions for velostatQueue */
osMessageQueueId_t velostatQueueHandle;
const osMessageQueueAttr_t velostatQueue_attributes =
		{ .name = "velostatQueue" };
/* Definitions for handQueue */
osMessageQueueId_t handQueueHandle;
const osMessageQueueAttr_t handQueue_attributes = { .name = "handQueue" };
/* Definitions for myTimer01 */
osTimerId_t myTimer01Handle;
const osTimerAttr_t myTimer01_attributes = { .name = "myTimer01" };
/* Definitions for handEvent */
osEventFlagsId_t handEventHandle;
const osEventFlagsAttr_t handEvent_attributes = { .name = "handEvent" };
/* Definitions for velostatEvent */
osEventFlagsId_t velostatEventHandle;
const osEventFlagsAttr_t velostatEvent_attributes = { .name = "velostatEvent" };
/* Definitions for stopServoEvent */
osEventFlagsId_t stopServoEventHandle;
const osEventFlagsAttr_t stopServoEvent_attributes =
		{ .name = "stopServoEvent" };
/* USER CODE BEGIN PV */
uint32_t *ptrCellsBuffer;
uint16_t *ptrTxBuffer;
uint16_t scanRowth;
volatile uint16_t isRun;
//servo
Servo *bot1, *top1, *bot2, *top2, *bot3, *top3;
uint8_t *rxData;
volatile uint16_t first;
volatile uint32_t countRun = 0u;
volatile uint32_t flagOnce = 1u;
volatile uint16_t peakControlMode;
volatile uint16_t flagTimer;
volatile uint32_t flag = 0u;
MyArray *matrix, *botArray, *topArray, *palmArray;
MyArray *bot1Array, *bot2Array, *bot3Array;
MyArray *top1Array, *top2Array, *top3Array;
volatile uint16_t threshHoldValue;
volatile uint16_t sumThreshHold;
uint16_t timeDelay;
volatile uint16_t maxBot, maxTop, maxPalm;
volatile uint16_t maxBot1, maxBot2, maxBot3;
volatile uint16_t maxTop1, maxTop2, maxTop3;
volatile uint8_t flag12, flag13, flag123;
volatile uint16_t sumBot, sumTop, countSum;
uint16_t threshHold[20] = { 0, 100, 200, 300, 400, 500, 600, 700, 800, 900,
		1000, 1100, 1200, 1300, 1400, 1500, 1600, 1700, 1800, 1900 };
//button
volatile uint16_t isPress;
volatile uint16_t btn;
//debug
volatile uint32_t start, end, startScan, endScan, startTx, endTx;
/* USER CODE END PV */

/* Private function prototypes -----------------------------------------------*/
void SystemClock_Config(void);
static void MX_GPIO_Init(void);
static void MX_DMA_Init(void);
static void MX_ADC1_Init(void);
static void MX_USART1_UART_Init(void);
static void MX_TIM2_Init(void);
static void MX_TIM3_Init(void);
static void MX_TIM4_Init(void);
static void MX_ADC2_Init(void);
void StartVelostatScanADC(void *argument);
void StartVelostatProcessOutput(void *argument);
void StartVelostatTransmit(void *argument);
void StartHandBot1(void *argument);
void StartHandTop1(void *argument);
void StartHandBot2(void *argument);
void StartHandTop2(void *argument);
void StartHandBot3(void *argument);
void StartHandTop3(void *argument);
void StartHandRxHandler(void *argument);
void StartHandMatrixHandler(void *argument);
void StartHandPeakControl(void *argument);
void StartButtonHandler(void *argument);
void StartHandStopServo(void *argument);
void Callback01(void *argument);

/* USER CODE BEGIN PFP */
void HAL_ADC_ConvCpltCallback(ADC_HandleTypeDef *hadc);
void User_DMA_XferCpltCallback(DMA_HandleTypeDef *hdma);
void HAL_UART_RxCpltCallback(UART_HandleTypeDef *huart);
void HAL_UART_TxCpltCallback(UART_HandleTypeDef *huart);
void HAL_GPIO_EXTI_Callback(uint16_t GPIO_Pin);
void vApplicationMallocFailedHook(void);
/* USER CODE END PFP */

/* Private user code ---------------------------------------------------------*/
/* USER CODE BEGIN 0 */
/* USER CODE END 0 */

/**
 * @brief  The application entry point.
 * @retval int
 */
int main(void) {
	/* USER CODE BEGIN 1 */

	/* USER CODE END 1 */

	/* MCU Configuration--------------------------------------------------------*/

	/* Reset of all peripherals, Initializes the Flash interface and the Systick. */
	HAL_Init();

	/* USER CODE BEGIN Init */
	scanRowth = 0u;
	first = 1u;
	peakControlMode = 0u;
	flagTimer = 1u;
	timeDelay = 200u;
	//button
	isPress = 0u;
	/* USER CODE END Init */

	/* Configure the system clock */
	SystemClock_Config();

	/* USER CODE BEGIN SysInit */

	/* USER CODE END SysInit */

	/* Initialize all configured peripherals */
	MX_GPIO_Init();
	MX_DMA_Init();
	MX_ADC1_Init();
	MX_USART1_UART_Init();
	MX_TIM2_Init();
	MX_TIM3_Init();
	MX_TIM4_Init();
	MX_ADC2_Init();
	/* USER CODE BEGIN 2 */

	/* USER CODE END 2 */

	/* Init scheduler */
	osKernelInitialize();

	/* USER CODE BEGIN RTOS_MUTEX */
	/* add mutexes, ... */
	/* USER CODE END RTOS_MUTEX */

	/* USER CODE BEGIN RTOS_SEMAPHORES */
	/* add semaphores, ... */
	/* USER CODE END RTOS_SEMAPHORES */

	/* Create the timer(s) */
	/* creation of myTimer01 */
	myTimer01Handle = osTimerNew(Callback01, osTimerOnce, NULL,
			&myTimer01_attributes);

	/* USER CODE BEGIN RTOS_TIMERS */
	/* start timers, add new ones, ... */
	/* USER CODE END RTOS_TIMERS */

	/* Create the queue(s) */
	/* creation of velostatQueue */
	velostatQueueHandle = osMessageQueueNew(3, sizeof(uint16_t*),
			&velostatQueue_attributes);

	/* creation of handQueue */
	handQueueHandle = osMessageQueueNew(3, sizeof(uint16_t*),
			&handQueue_attributes);

	/* USER CODE BEGIN RTOS_QUEUES */
	/* add queues, ... */
	/* USER CODE END RTOS_QUEUES */

	/* Create the thread(s) */
	/* creation of VelostatScanADC */
	VelostatScanADCHandle = osThreadNew(StartVelostatScanADC, NULL,
			&VelostatScanADC_attributes);

	/* creation of VelostatProcessOutput */
	VelostatProcessOutputHandle = osThreadNew(StartVelostatProcessOutput, NULL,
			&VelostatProcessOutput_attributes);

	/* creation of VelostatTransmit */
	VelostatTransmitHandle = osThreadNew(StartVelostatTransmit, NULL,
			&VelostatTransmit_attributes);

	/* creation of HandBot1 */
	HandBot1Handle = osThreadNew(StartHandBot1, NULL, &HandBot1_attributes);

	/* creation of HandTop1 */
	HandTop1Handle = osThreadNew(StartHandTop1, NULL, &HandTop1_attributes);

	/* creation of HandBot2 */
	HandBot2Handle = osThreadNew(StartHandBot2, NULL, &HandBot2_attributes);

	/* creation of HandTop2 */
	HandTop2Handle = osThreadNew(StartHandTop2, NULL, &HandTop2_attributes);

	/* creation of HandBot3 */
	HandBot3Handle = osThreadNew(StartHandBot3, NULL, &HandBot3_attributes);

	/* creation of HandTop3 */
	HandTop3Handle = osThreadNew(StartHandTop3, NULL, &HandTop3_attributes);

	/* creation of HandRxHandler */
	HandRxHandlerHandle = osThreadNew(StartHandRxHandler, NULL,
			&HandRxHandler_attributes);

	/* creation of HandMatrixHandler */
	HandMatrixHandlerHandle = osThreadNew(StartHandMatrixHandler, NULL,
			&HandMatrixHandler_attributes);

	/* creation of HandPeakControl */
	HandPeakControlHandle = osThreadNew(StartHandPeakControl, NULL,
			&HandPeakControl_attributes);

	/* creation of ButtonHandler */
	ButtonHandlerHandle = osThreadNew(StartButtonHandler, NULL,
			&ButtonHandler_attributes);

	/* creation of HandStopServo */
	HandStopServoHandle = osThreadNew(StartHandStopServo, NULL,
			&HandStopServo_attributes);

	/* USER CODE BEGIN RTOS_THREADS */
	/* add threads, ... */
	/* USER CODE END RTOS_THREADS */

	/* Create the event(s) */
	/* creation of handEvent */
	handEventHandle = osEventFlagsNew(&handEvent_attributes);

	/* creation of velostatEvent */
	velostatEventHandle = osEventFlagsNew(&velostatEvent_attributes);

	/* creation of stopServoEvent */
	stopServoEventHandle = osEventFlagsNew(&stopServoEvent_attributes);

	/* USER CODE BEGIN RTOS_EVENTS */
	/* add events, ... */
	/* USER CODE END RTOS_EVENTS */

	/* Start scheduler */
	osKernelStart();

	/* We should never get here as control is now taken by the scheduler */
	/* Infinite loop */
	/* USER CODE BEGIN WHILE */
	while (1) {
		/* USER CODE END WHILE */

		/* USER CODE BEGIN 3 */
	}
	/* USER CODE END 3 */
}

/**
 * @brief System Clock Configuration
 * @retval None
 */
void SystemClock_Config(void) {
	RCC_OscInitTypeDef RCC_OscInitStruct = { 0 };
	RCC_ClkInitTypeDef RCC_ClkInitStruct = { 0 };
	RCC_PeriphCLKInitTypeDef PeriphClkInit = { 0 };

	/** Initializes the RCC Oscillators according to the specified parameters
	 * in the RCC_OscInitTypeDef structure.
	 */
	RCC_OscInitStruct.OscillatorType = RCC_OSCILLATORTYPE_HSE;
	RCC_OscInitStruct.HSEState = RCC_HSE_ON;
	RCC_OscInitStruct.HSEPredivValue = RCC_HSE_PREDIV_DIV1;
	RCC_OscInitStruct.HSIState = RCC_HSI_ON;
	RCC_OscInitStruct.PLL.PLLState = RCC_PLL_ON;
	RCC_OscInitStruct.PLL.PLLSource = RCC_PLLSOURCE_HSE;
	RCC_OscInitStruct.PLL.PLLMUL = RCC_PLL_MUL9;
	if (HAL_RCC_OscConfig(&RCC_OscInitStruct) != HAL_OK) {
		Error_Handler();
	}
	/** Initializes the CPU, AHB and APB buses clocks
	 */
	RCC_ClkInitStruct.ClockType = RCC_CLOCKTYPE_HCLK | RCC_CLOCKTYPE_SYSCLK
			| RCC_CLOCKTYPE_PCLK1 | RCC_CLOCKTYPE_PCLK2;
	RCC_ClkInitStruct.SYSCLKSource = RCC_SYSCLKSOURCE_PLLCLK;
	RCC_ClkInitStruct.AHBCLKDivider = RCC_SYSCLK_DIV1;
	RCC_ClkInitStruct.APB1CLKDivider = RCC_HCLK_DIV2;
	RCC_ClkInitStruct.APB2CLKDivider = RCC_HCLK_DIV1;

	if (HAL_RCC_ClockConfig(&RCC_ClkInitStruct, FLASH_LATENCY_2) != HAL_OK) {
		Error_Handler();
	}
	PeriphClkInit.PeriphClockSelection = RCC_PERIPHCLK_ADC;
	PeriphClkInit.AdcClockSelection = RCC_ADCPCLK2_DIV6;
	if (HAL_RCCEx_PeriphCLKConfig(&PeriphClkInit) != HAL_OK) {
		Error_Handler();
	}
}

/**
 * @brief ADC1 Initialization Function
 * @param None
 * @retval None
 */
static void MX_ADC1_Init(void) {

	/* USER CODE BEGIN ADC1_Init 0 */

	/* USER CODE END ADC1_Init 0 */

	ADC_MultiModeTypeDef multimode = { 0 };
	ADC_ChannelConfTypeDef sConfig = { 0 };

	/* USER CODE BEGIN ADC1_Init 1 */

	/* USER CODE END ADC1_Init 1 */
	/** Common config
	 */
	hadc1.Instance = ADC1;
	hadc1.Init.ScanConvMode = ADC_SCAN_ENABLE;
	hadc1.Init.ContinuousConvMode = DISABLE;
	hadc1.Init.DiscontinuousConvMode = DISABLE;
	hadc1.Init.ExternalTrigConv = ADC_SOFTWARE_START;
	hadc1.Init.DataAlign = ADC_DATAALIGN_RIGHT;
	hadc1.Init.NbrOfConversion = 5;
	if (HAL_ADC_Init(&hadc1) != HAL_OK) {
		Error_Handler();
	}
	/** Configure the ADC multi-mode
	 */
	multimode.Mode = ADC_DUALMODE_REGSIMULT;
	if (HAL_ADCEx_MultiModeConfigChannel(&hadc1, &multimode) != HAL_OK) {
		Error_Handler();
	}
	/** Configure Regular Channel
	 */
	sConfig.Channel = ADC_CHANNEL_0;
	sConfig.Rank = ADC_REGULAR_RANK_1;
	sConfig.SamplingTime = ADC_SAMPLETIME_239CYCLES_5;
	if (HAL_ADC_ConfigChannel(&hadc1, &sConfig) != HAL_OK) {
		Error_Handler();
	}
	/** Configure Regular Channel
	 */
	sConfig.Channel = ADC_CHANNEL_2;
	sConfig.Rank = ADC_REGULAR_RANK_2;
	if (HAL_ADC_ConfigChannel(&hadc1, &sConfig) != HAL_OK) {
		Error_Handler();
	}
	/** Configure Regular Channel
	 */
	sConfig.Channel = ADC_CHANNEL_4;
	sConfig.Rank = ADC_REGULAR_RANK_3;
	if (HAL_ADC_ConfigChannel(&hadc1, &sConfig) != HAL_OK) {
		Error_Handler();
	}
	/** Configure Regular Channel
	 */
	sConfig.Channel = ADC_CHANNEL_6;
	sConfig.Rank = ADC_REGULAR_RANK_4;
	if (HAL_ADC_ConfigChannel(&hadc1, &sConfig) != HAL_OK) {
		Error_Handler();
	}
	/** Configure Regular Channel
	 */
	sConfig.Channel = ADC_CHANNEL_8;
	sConfig.Rank = ADC_REGULAR_RANK_5;
	if (HAL_ADC_ConfigChannel(&hadc1, &sConfig) != HAL_OK) {
		Error_Handler();
	}
	/* USER CODE BEGIN ADC1_Init 2 */

	/* USER CODE END ADC1_Init 2 */

}

/**
 * @brief ADC2 Initialization Function
 * @param None
 * @retval None
 */
static void MX_ADC2_Init(void) {

	/* USER CODE BEGIN ADC2_Init 0 */

	/* USER CODE END ADC2_Init 0 */

	ADC_ChannelConfTypeDef sConfig = { 0 };

	/* USER CODE BEGIN ADC2_Init 1 */

	/* USER CODE END ADC2_Init 1 */
	/** Common config
	 */
	hadc2.Instance = ADC2;
	hadc2.Init.ScanConvMode = ADC_SCAN_ENABLE;
	hadc2.Init.ContinuousConvMode = DISABLE;
	hadc2.Init.DiscontinuousConvMode = DISABLE;
	hadc2.Init.ExternalTrigConv = ADC_SOFTWARE_START;
	hadc2.Init.DataAlign = ADC_DATAALIGN_RIGHT;
	hadc2.Init.NbrOfConversion = 5;
	if (HAL_ADC_Init(&hadc2) != HAL_OK) {
		Error_Handler();
	}
	/** Configure Regular Channel
	 */
	sConfig.Channel = ADC_CHANNEL_1;
	sConfig.Rank = ADC_REGULAR_RANK_1;
	sConfig.SamplingTime = ADC_SAMPLETIME_239CYCLES_5;
	if (HAL_ADC_ConfigChannel(&hadc2, &sConfig) != HAL_OK) {
		Error_Handler();
	}
	/** Configure Regular Channel
	 */
	sConfig.Channel = ADC_CHANNEL_3;
	sConfig.Rank = ADC_REGULAR_RANK_2;
	if (HAL_ADC_ConfigChannel(&hadc2, &sConfig) != HAL_OK) {
		Error_Handler();
	}
	/** Configure Regular Channel
	 */
	sConfig.Channel = ADC_CHANNEL_5;
	sConfig.Rank = ADC_REGULAR_RANK_3;
	if (HAL_ADC_ConfigChannel(&hadc2, &sConfig) != HAL_OK) {
		Error_Handler();
	}
	/** Configure Regular Channel
	 */
	sConfig.Channel = ADC_CHANNEL_7;
	sConfig.Rank = ADC_REGULAR_RANK_4;
	if (HAL_ADC_ConfigChannel(&hadc2, &sConfig) != HAL_OK) {
		Error_Handler();
	}
	/** Configure Regular Channel
	 */
	sConfig.Channel = ADC_CHANNEL_9;
	sConfig.Rank = ADC_REGULAR_RANK_5;
	if (HAL_ADC_ConfigChannel(&hadc2, &sConfig) != HAL_OK) {
		Error_Handler();
	}
	/* USER CODE BEGIN ADC2_Init 2 */

	/* USER CODE END ADC2_Init 2 */

}

/**
 * @brief TIM2 Initialization Function
 * @param None
 * @retval None
 */
static void MX_TIM2_Init(void) {

	/* USER CODE BEGIN TIM2_Init 0 */

	/* USER CODE END TIM2_Init 0 */

	TIM_ClockConfigTypeDef sClockSourceConfig = { 0 };
	TIM_MasterConfigTypeDef sMasterConfig = { 0 };
	TIM_OC_InitTypeDef sConfigOC = { 0 };

	/* USER CODE BEGIN TIM2_Init 1 */

	/* USER CODE END TIM2_Init 1 */
	htim2.Instance = TIM2;
	htim2.Init.Prescaler = 1440 - 1;
	htim2.Init.CounterMode = TIM_COUNTERMODE_UP;
	htim2.Init.Period = 1000 - 1;
	htim2.Init.ClockDivision = TIM_CLOCKDIVISION_DIV1;
	htim2.Init.AutoReloadPreload = TIM_AUTORELOAD_PRELOAD_ENABLE;
	if (HAL_TIM_Base_Init(&htim2) != HAL_OK) {
		Error_Handler();
	}
	sClockSourceConfig.ClockSource = TIM_CLOCKSOURCE_INTERNAL;
	if (HAL_TIM_ConfigClockSource(&htim2, &sClockSourceConfig) != HAL_OK) {
		Error_Handler();
	}
	if (HAL_TIM_PWM_Init(&htim2) != HAL_OK) {
		Error_Handler();
	}
	sMasterConfig.MasterOutputTrigger = TIM_TRGO_RESET;
	sMasterConfig.MasterSlaveMode = TIM_MASTERSLAVEMODE_DISABLE;
	if (HAL_TIMEx_MasterConfigSynchronization(&htim2, &sMasterConfig)
			!= HAL_OK) {
		Error_Handler();
	}
	sConfigOC.OCMode = TIM_OCMODE_PWM1;
	sConfigOC.Pulse = 0;
	sConfigOC.OCPolarity = TIM_OCPOLARITY_HIGH;
	sConfigOC.OCFastMode = TIM_OCFAST_DISABLE;
	if (HAL_TIM_PWM_ConfigChannel(&htim2, &sConfigOC, TIM_CHANNEL_1)
			!= HAL_OK) {
		Error_Handler();
	}
	if (HAL_TIM_PWM_ConfigChannel(&htim2, &sConfigOC, TIM_CHANNEL_2)
			!= HAL_OK) {
		Error_Handler();
	}
	/* USER CODE BEGIN TIM2_Init 2 */

	/* USER CODE END TIM2_Init 2 */
	HAL_TIM_MspPostInit(&htim2);

}

/**
 * @brief TIM3 Initialization Function
 * @param None
 * @retval None
 */
static void MX_TIM3_Init(void) {

	/* USER CODE BEGIN TIM3_Init 0 */

	/* USER CODE END TIM3_Init 0 */

	TIM_ClockConfigTypeDef sClockSourceConfig = { 0 };
	TIM_MasterConfigTypeDef sMasterConfig = { 0 };
	TIM_OC_InitTypeDef sConfigOC = { 0 };

	/* USER CODE BEGIN TIM3_Init 1 */

	/* USER CODE END TIM3_Init 1 */
	htim3.Instance = TIM3;
	htim3.Init.Prescaler = 1440 - 1;
	htim3.Init.CounterMode = TIM_COUNTERMODE_UP;
	htim3.Init.Period = 1000 - 1;
	htim3.Init.ClockDivision = TIM_CLOCKDIVISION_DIV1;
	htim3.Init.AutoReloadPreload = TIM_AUTORELOAD_PRELOAD_ENABLE;
	if (HAL_TIM_Base_Init(&htim3) != HAL_OK) {
		Error_Handler();
	}
	sClockSourceConfig.ClockSource = TIM_CLOCKSOURCE_INTERNAL;
	if (HAL_TIM_ConfigClockSource(&htim3, &sClockSourceConfig) != HAL_OK) {
		Error_Handler();
	}
	if (HAL_TIM_PWM_Init(&htim3) != HAL_OK) {
		Error_Handler();
	}
	sMasterConfig.MasterOutputTrigger = TIM_TRGO_RESET;
	sMasterConfig.MasterSlaveMode = TIM_MASTERSLAVEMODE_DISABLE;
	if (HAL_TIMEx_MasterConfigSynchronization(&htim3, &sMasterConfig)
			!= HAL_OK) {
		Error_Handler();
	}
	sConfigOC.OCMode = TIM_OCMODE_PWM1;
	sConfigOC.Pulse = 0;
	sConfigOC.OCPolarity = TIM_OCPOLARITY_HIGH;
	sConfigOC.OCFastMode = TIM_OCFAST_DISABLE;
	if (HAL_TIM_PWM_ConfigChannel(&htim3, &sConfigOC, TIM_CHANNEL_1)
			!= HAL_OK) {
		Error_Handler();
	}
	if (HAL_TIM_PWM_ConfigChannel(&htim3, &sConfigOC, TIM_CHANNEL_2)
			!= HAL_OK) {
		Error_Handler();
	}
	/* USER CODE BEGIN TIM3_Init 2 */

	/* USER CODE END TIM3_Init 2 */
	HAL_TIM_MspPostInit(&htim3);

}

/**
 * @brief TIM4 Initialization Function
 * @param None
 * @retval None
 */
static void MX_TIM4_Init(void) {

	/* USER CODE BEGIN TIM4_Init 0 */

	/* USER CODE END TIM4_Init 0 */

	TIM_ClockConfigTypeDef sClockSourceConfig = { 0 };
	TIM_MasterConfigTypeDef sMasterConfig = { 0 };
	TIM_OC_InitTypeDef sConfigOC = { 0 };

	/* USER CODE BEGIN TIM4_Init 1 */

	/* USER CODE END TIM4_Init 1 */
	htim4.Instance = TIM4;
	htim4.Init.Prescaler = 1440 - 1;
	htim4.Init.CounterMode = TIM_COUNTERMODE_UP;
	htim4.Init.Period = 1000 - 1;
	htim4.Init.ClockDivision = TIM_CLOCKDIVISION_DIV1;
	htim4.Init.AutoReloadPreload = TIM_AUTORELOAD_PRELOAD_DISABLE;
	if (HAL_TIM_Base_Init(&htim4) != HAL_OK) {
		Error_Handler();
	}
	sClockSourceConfig.ClockSource = TIM_CLOCKSOURCE_INTERNAL;
	if (HAL_TIM_ConfigClockSource(&htim4, &sClockSourceConfig) != HAL_OK) {
		Error_Handler();
	}
	if (HAL_TIM_PWM_Init(&htim4) != HAL_OK) {
		Error_Handler();
	}
	sMasterConfig.MasterOutputTrigger = TIM_TRGO_RESET;
	sMasterConfig.MasterSlaveMode = TIM_MASTERSLAVEMODE_DISABLE;
	if (HAL_TIMEx_MasterConfigSynchronization(&htim4, &sMasterConfig)
			!= HAL_OK) {
		Error_Handler();
	}
	sConfigOC.OCMode = TIM_OCMODE_PWM1;
	sConfigOC.Pulse = 0;
	sConfigOC.OCPolarity = TIM_OCPOLARITY_HIGH;
	sConfigOC.OCFastMode = TIM_OCFAST_DISABLE;
	if (HAL_TIM_PWM_ConfigChannel(&htim4, &sConfigOC, TIM_CHANNEL_1)
			!= HAL_OK) {
		Error_Handler();
	}
	if (HAL_TIM_PWM_ConfigChannel(&htim4, &sConfigOC, TIM_CHANNEL_2)
			!= HAL_OK) {
		Error_Handler();
	}
	/* USER CODE BEGIN TIM4_Init 2 */

	/* USER CODE END TIM4_Init 2 */
	HAL_TIM_MspPostInit(&htim4);

}

/**
 * @brief USART1 Initialization Function
 * @param None
 * @retval None
 */
static void MX_USART1_UART_Init(void) {

	/* USER CODE BEGIN USART1_Init 0 */

	/* USER CODE END USART1_Init 0 */

	/* USER CODE BEGIN USART1_Init 1 */

	/* USER CODE END USART1_Init 1 */
	huart1.Instance = USART1;
	huart1.Init.BaudRate = 115200;
	huart1.Init.WordLength = UART_WORDLENGTH_8B;
	huart1.Init.StopBits = UART_STOPBITS_1;
	huart1.Init.Parity = UART_PARITY_NONE;
	huart1.Init.Mode = UART_MODE_TX_RX;
	huart1.Init.HwFlowCtl = UART_HWCONTROL_NONE;
	huart1.Init.OverSampling = UART_OVERSAMPLING_16;
	if (HAL_UART_Init(&huart1) != HAL_OK) {
		Error_Handler();
	}
	/* USER CODE BEGIN USART1_Init 2 */

	/* USER CODE END USART1_Init 2 */

}

/**
 * Enable DMA controller clock
 * Configure DMA for memory to memory transfers
 *   hdma_memtomem_dma1_channel2
 *   hdma_memtomem_dma1_channel3
 */
static void MX_DMA_Init(void) {

	/* DMA controller clock enable */
	__HAL_RCC_DMA1_CLK_ENABLE();

	/* Configure DMA request hdma_memtomem_dma1_channel2 on DMA1_Channel2 */
	hdma_memtomem_dma1_channel2.Instance = DMA1_Channel2;
	hdma_memtomem_dma1_channel2.Init.Direction = DMA_MEMORY_TO_MEMORY;
	hdma_memtomem_dma1_channel2.Init.PeriphInc = DMA_PINC_ENABLE;
	hdma_memtomem_dma1_channel2.Init.MemInc = DMA_MINC_ENABLE;
	hdma_memtomem_dma1_channel2.Init.PeriphDataAlignment =
	DMA_PDATAALIGN_HALFWORD;
	hdma_memtomem_dma1_channel2.Init.MemDataAlignment = DMA_MDATAALIGN_HALFWORD;
	hdma_memtomem_dma1_channel2.Init.Mode = DMA_NORMAL;
	hdma_memtomem_dma1_channel2.Init.Priority = DMA_PRIORITY_LOW;
	if (HAL_DMA_Init(&hdma_memtomem_dma1_channel2) != HAL_OK) {
		Error_Handler();
	}

	/* Configure DMA request hdma_memtomem_dma1_channel3 on DMA1_Channel3 */
	hdma_memtomem_dma1_channel3.Instance = DMA1_Channel3;
	hdma_memtomem_dma1_channel3.Init.Direction = DMA_MEMORY_TO_MEMORY;
	hdma_memtomem_dma1_channel3.Init.PeriphInc = DMA_PINC_ENABLE;
	hdma_memtomem_dma1_channel3.Init.MemInc = DMA_MINC_ENABLE;
	hdma_memtomem_dma1_channel3.Init.PeriphDataAlignment =
	DMA_PDATAALIGN_HALFWORD;
	hdma_memtomem_dma1_channel3.Init.MemDataAlignment = DMA_MDATAALIGN_HALFWORD;
	hdma_memtomem_dma1_channel3.Init.Mode = DMA_NORMAL;
	hdma_memtomem_dma1_channel3.Init.Priority = DMA_PRIORITY_MEDIUM;
	if (HAL_DMA_Init(&hdma_memtomem_dma1_channel3) != HAL_OK) {
		Error_Handler();
	}

	/* DMA interrupt init */
	/* DMA1_Channel1_IRQn interrupt configuration */
	HAL_NVIC_SetPriority(DMA1_Channel1_IRQn, 5, 0);
	HAL_NVIC_EnableIRQ(DMA1_Channel1_IRQn);
	/* DMA1_Channel2_IRQn interrupt configuration */
	HAL_NVIC_SetPriority(DMA1_Channel2_IRQn, 5, 0);
	HAL_NVIC_EnableIRQ(DMA1_Channel2_IRQn);
	/* DMA1_Channel3_IRQn interrupt configuration */
	HAL_NVIC_SetPriority(DMA1_Channel3_IRQn, 5, 0);
	HAL_NVIC_EnableIRQ(DMA1_Channel3_IRQn);
	/* DMA1_Channel4_IRQn interrupt configuration */
	HAL_NVIC_SetPriority(DMA1_Channel4_IRQn, 5, 0);
	HAL_NVIC_EnableIRQ(DMA1_Channel4_IRQn);
	/* DMA1_Channel5_IRQn interrupt configuration */
	HAL_NVIC_SetPriority(DMA1_Channel5_IRQn, 5, 0);
	HAL_NVIC_EnableIRQ(DMA1_Channel5_IRQn);

}

/**
 * @brief GPIO Initialization Function
 * @param None
 * @retval None
 */
static void MX_GPIO_Init(void) {
	GPIO_InitTypeDef GPIO_InitStruct = { 0 };

	/* GPIO Ports Clock Enable */
	__HAL_RCC_GPIOC_CLK_ENABLE();
	__HAL_RCC_GPIOD_CLK_ENABLE();
	__HAL_RCC_GPIOA_CLK_ENABLE();
	__HAL_RCC_GPIOB_CLK_ENABLE();

	/*Configure GPIO pin Output Level */
	HAL_GPIO_WritePin(LEDGreen_GPIO_Port, LEDGreen_Pin, GPIO_PIN_RESET);

	/*Configure GPIO pin Output Level */
	HAL_GPIO_WritePin(GPIOB,
	A_Scan_Pin | B_Scan_Pin | C_Scan_Pin | D_Scan_Pin | E_Scan_Pin,
			GPIO_PIN_RESET);

	/*Configure GPIO pin Output Level */
	HAL_GPIO_WritePin(F_Scan_GPIO_Port, F_Scan_Pin, GPIO_PIN_RESET);

	/*Configure GPIO pin : LEDGreen_Pin */
	GPIO_InitStruct.Pin = LEDGreen_Pin;
	GPIO_InitStruct.Mode = GPIO_MODE_OUTPUT_PP;
	GPIO_InitStruct.Pull = GPIO_NOPULL;
	GPIO_InitStruct.Speed = GPIO_SPEED_FREQ_LOW;
	HAL_GPIO_Init(LEDGreen_GPIO_Port, &GPIO_InitStruct);

	/*Configure GPIO pins : Btn0_Pin Btn1_Pin */
	GPIO_InitStruct.Pin = Btn0_Pin | Btn1_Pin;
	GPIO_InitStruct.Mode = GPIO_MODE_IT_FALLING;
	GPIO_InitStruct.Pull = GPIO_NOPULL;
	HAL_GPIO_Init(GPIOC, &GPIO_InitStruct);

	/*Configure GPIO pins : A_Scan_Pin B_Scan_Pin C_Scan_Pin D_Scan_Pin
	 E_Scan_Pin */
	GPIO_InitStruct.Pin = A_Scan_Pin | B_Scan_Pin | C_Scan_Pin | D_Scan_Pin
			| E_Scan_Pin;
	GPIO_InitStruct.Mode = GPIO_MODE_OUTPUT_PP;
	GPIO_InitStruct.Pull = GPIO_NOPULL;
	GPIO_InitStruct.Speed = GPIO_SPEED_FREQ_LOW;
	HAL_GPIO_Init(GPIOB, &GPIO_InitStruct);

	/*Configure GPIO pin : F_Scan_Pin */
	GPIO_InitStruct.Pin = F_Scan_Pin;
	GPIO_InitStruct.Mode = GPIO_MODE_OUTPUT_PP;
	GPIO_InitStruct.Pull = GPIO_NOPULL;
	GPIO_InitStruct.Speed = GPIO_SPEED_FREQ_LOW;
	HAL_GPIO_Init(F_Scan_GPIO_Port, &GPIO_InitStruct);

	/* EXTI interrupt init*/
	HAL_NVIC_SetPriority(EXTI15_10_IRQn, 5, 0);
	HAL_NVIC_EnableIRQ(EXTI15_10_IRQn);

}

/* USER CODE BEGIN 4 */
void HAL_ADC_ConvCpltCallback(ADC_HandleTypeDef *hadc) {
	if (hadc->Instance == ADC1)
		osEventFlagsSet(velostatEventHandle, FlagVelostatADC_ConvCplt);
}

void User_DMA_XferCpltCallback(DMA_HandleTypeDef *hdma) {
	if (hdma->Instance == hdma_memtomem_dma1_channel2.Instance) {
		osEventFlagsSet(velostatEventHandle, FlagVelostatDMAChn2_XferCplt);
	} else if (hdma->Instance == hdma_memtomem_dma1_channel3.Instance) {
		osEventFlagsSet(velostatEventHandle, FlagVelostatDMAChn3_XferCplt);
	}
}

void HAL_UART_TxCpltCallback(UART_HandleTypeDef *huart) {
	if (huart->Instance == USART1) {
//		osEventFlagsSet(velostatEventHandle, FlagVelostatUART_TxCplt);
		endTx = HAL_GetTick() - startTx;
	}
}

void HAL_UART_RxCpltCallback(UART_HandleTypeDef *huart) {
	if (huart->Instance == USART1) {
		osEventFlagsSet(handEventHandle, FlagHandUART_RxCplt);
		HAL_UART_Receive_DMA(huart, rxData, NumberOfRxData);
	}
}

void HAL_GPIO_EXTI_Callback(uint16_t GPIO_Pin) {
	if (flagTimer) {
		flagTimer = 0u;
		btn = GPIO_Pin;
		isPress = 1u;
		osThreadFlagsSet(ButtonHandlerHandle, 1u);
	}
}
/* USER CODE END 4 */

/* USER CODE BEGIN Header_StartVelostatScanADC */
/**
 * @brief  Function implementing the VelostatScanADC thread.
 * @param  argument: Not used
 * @retval None
 */
/* USER CODE END Header_StartVelostatScanADC */
void StartVelostatScanADC(void *argument) {
	/* USER CODE BEGIN 5 */
	ptrCellsBuffer = (uint32_t*) pvPortMalloc(
			(NumberOfCells / 2u) * sizeof(uint32_t));
	if (ptrCellsBuffer == NULL)
		Error_Handler();

	HAL_ADCEx_Calibration_Start(&hadc2);
	HAL_ADCEx_Calibration_Start(&hadc1);
	scanRowth = 0u;
	uint32_t *ptr;
	/* Infinite loop */
	for (;;) {
		//enable flags
		osEventFlagsWait(velostatEventHandle, FlagVelostatEnable,
		osFlagsWaitAll | osFlagsNoClear, osWaitForever);
		//
		osEventFlagsWait(velostatEventHandle,
		FlagVelostatSortDataNotFull | FlagVelostatEnqueue, osFlagsWaitAny,
				100u);

		Scan_74238(scanRowth);

		ptr = ptrCellsBuffer;
		ptr += (scanRowth * NumberOfConversion);

		HAL_ADC_Start(&hadc2);
		HAL_ADCEx_MultiModeStart_DMA(&hadc1, (uint32_t*) ptr,
		NumberOfConversion);
		//
		osEventFlagsWait(velostatEventHandle, FlagVelostatADC_ConvCplt,
		osFlagsWaitAll, osWaitForever);
		if (scanRowth >= (NumberOfScanLimit - 1)) {
			scanRowth -= (NumberOfScanLimit - 1);
			osEventFlagsSet(velostatEventHandle, FlagVelostatSortDataFull);
			endScan = HAL_GetTick() - startScan;
		} else {
			++scanRowth;
			osEventFlagsSet(velostatEventHandle, FlagVelostatSortDataNotFull);
		}
	}
	/* USER CODE END 5 */
}

/* USER CODE BEGIN Header_StartVelostatProcessOutput */
/**
 * @brief Function implementing the VelostatProcessOutput thread.
 * @param argument: Not used
 * @retval None
 */
/* USER CODE END Header_StartVelostatProcessOutput */
void StartVelostatProcessOutput(void *argument) {
	/* USER CODE BEGIN StartVelostatProcessOutput */
	osStatus_t stat;
	uint16_t *ptr;
	hdma_memtomem_dma1_channel2.XferCpltCallback = User_DMA_XferCpltCallback;
	/* Infinite loop */
	for (;;) {
		osEventFlagsWait(velostatEventHandle, FlagVelostatSortDataFull,
		osFlagsWaitAll, osWaitForever);

//		volatile uint32_t heapSize = xPortGetFreeHeapSize();

		ptr = (uint16_t*) pvPortMalloc(NumberOfCells * sizeof(uint16_t));
		if (ptr == NULL)
			Error_Handler();

		HAL_DMA_Start_IT(&hdma_memtomem_dma1_channel2,
				(uint32_t) ptrCellsBuffer, (uint32_t) ptr,
				NumberOfCells);
		//
		osEventFlagsWait(velostatEventHandle, FlagVelostatDMAChn2_XferCplt,
		osFlagsWaitAll, osWaitForever);

		//bot3 row 4, col 8 and col 9
		ptr[85u] = ptr[86u];
		ptr[95u] = ptr[105u];

		//queue put timeout before FlagVelostatSortDataFull
		stat = osMessageQueuePut(velostatQueueHandle, &ptr, 0, 35u);
		if (stat == osErrorTimeout) {
			vPortFree(ptr);
		} else if (stat == osOK) {
			osEventFlagsSet(velostatEventHandle, FlagVelostatEnqueue);
			startScan = HAL_GetTick();
		}
	}
	/* USER CODE END StartVelostatProcessOutput */
}

/* USER CODE BEGIN Header_StartVelostatTransmit */
/**
 * @brief Function implementing the VelostatTransmit thread.
 * @param argument: Not used
 * @retval None
 */
/* USER CODE END Header_StartVelostatTransmit */
void StartVelostatTransmit(void *argument) {
	/* USER CODE BEGIN StartVelostatTransmit */
	uint16_t *buffer = (uint16_t*) pvPortMalloc(
	TxBufferSize * sizeof(uint16_t));
	if (buffer == NULL)
		Error_Handler();

	buffer[0] = 0xaaaa;
	buffer[TxBufferSize - 1] = 0x5555;

	uint16_t *buf;
	osStatus_t stat;
	hdma_memtomem_dma1_channel3.XferCpltCallback = User_DMA_XferCpltCallback;
	/* Infinite loop */
	for (;;) {
		stat = osMessageQueueGet(velostatQueueHandle, &buf, NULL,
		osWaitForever);
		if (stat == osOK) {
			startTx = HAL_GetTick();
			HAL_DMA_Start_IT(&hdma_memtomem_dma1_channel3, (uint32_t) buf,
					(uint32_t) (buffer + 1), NumberOfCells);
			//
			osEventFlagsWait(velostatEventHandle, FlagVelostatDMAChn3_XferCplt,
			osFlagsWaitAll, osWaitForever);

			//pass buf to handQueueHandle
			if (osMessageQueuePut(handQueueHandle, &buf, 0, 50u) != osOK)
				vPortFree(buf);

			//transmit here
			HAL_UART_Transmit_DMA(&huart1, (uint8_t*) buffer,
			TxBufferSize * 2);
		}
	}
	/* USER CODE END StartVelostatTransmit */
}

/* USER CODE BEGIN Header_StartHandBot1 */
/**
 * @brief Function implementing the HandBot1 thread.
 * @param argument: Not used
 * @retval None
 */
/* USER CODE END Header_StartHandBot1 */
void StartHandBot1(void *argument) {
	/* USER CODE BEGIN StartHandBot1 */
	bot1 = (Servo*) pvPortMalloc(sizeof(Servo));
	if (bot1 == NULL)
		Error_Handler();
	vServoInit(bot1, &htim2, TIM_CHANNEL_1, handEventHandle, FlagHandBot1);

	bot1->downLimit = 41u;
	bot1->upLimit = 66u;
	bot1->isReverse = 0u;

	vServoStart(bot1);
	/* Infinite loop */
	for (;;) {
		eServoMove(bot1);
//		if (!peakControlMode) {
//			osDelay(bot1->delay);
//		} else {
//			osDelay(bot1->delay * 4u);
////			osDelay(1u);
//		}
//		osDelay(bot1->delay);
	}
	/* USER CODE END StartHandBot1 */
}

/* USER CODE BEGIN Header_StartHandTop1 */
/**
 * @brief Function implementing the HandTop1 thread.
 * @param argument: Not used
 * @retval None
 */
/* USER CODE END Header_StartHandTop1 */
void StartHandTop1(void *argument) {
	/* USER CODE BEGIN StartHandTop1 */
	top1 = (Servo*) pvPortMalloc(sizeof(Servo));
	if (top1 == NULL)
		Error_Handler();
	vServoInit(top1, &htim2, TIM_CHANNEL_2, handEventHandle, FlagHandTop1);

	top1->downLimit = 28u;
	top1->upLimit = 84u;
	top1->isReverse = 1u;

	vServoStart(top1);
	/* Infinite loop */
	for (;;) {
		eServoMove(top1);
//		if (!peakControlMode) {
//			osDelay(top1->delay);
//		} else {
//			osDelay(top1->delay * 4u);
//		}
//		osDelay(top1->delay);
	}
	/* USER CODE END StartHandTop1 */
}

/* USER CODE BEGIN Header_StartHandBot2 */
/**
 * @brief Function implementing the HandBot2 thread.
 * @param argument: Not used
 * @retval None
 */
/* USER CODE END Header_StartHandBot2 */
void StartHandBot2(void *argument) {
	/* USER CODE BEGIN StartHandBot2 */
	bot2 = (Servo*) pvPortMalloc(sizeof(Servo));
	if (bot2 == NULL)
		Error_Handler();
	vServoInit(bot2, &htim3, TIM_CHANNEL_1, handEventHandle, FlagHandBot2);

	bot2->downLimit = 45u;
	bot2->upLimit = 78u;
	bot2->isReverse = 0u;

	vServoStart(bot2);
	/* Infinite loop */
	for (;;) {
		eServoMove(bot2);
//		if (!peakControlMode) {
//			osDelay(bot2->delay);
//		} else {
//			osDelay(bot2->delay * 4u);
//		}
//		osDelay(bot2->delay);
	}
	/* USER CODE END StartHandBot2 */
}

/* USER CODE BEGIN Header_StartHandTop2 */
/**
 * @brief Function implementing the HandTop2 thread.
 * @param argument: Not used
 * @retval None
 */
/* USER CODE END Header_StartHandTop2 */
void StartHandTop2(void *argument) {
	/* USER CODE BEGIN StartHandTop2 */
	top2 = (Servo*) pvPortMalloc(sizeof(Servo));
	if (top2 == NULL)
		Error_Handler();
	vServoInit(top2, &htim3, TIM_CHANNEL_2, handEventHandle, FlagHandTop2);

	top2->downLimit = 34u;
	top2->upLimit = 89u;
	top2->isReverse = 0u;

	vServoStart(top2);
	/* Infinite loop */
	for (;;) {
		eServoMove(top2);
//		if (!peakControlMode) {
//			osDelay(top2->delay);
//		} else {
//			osDelay(top2->delay * 4u);
//		}
//		osDelay(top2->delay);
	}
	/* USER CODE END StartHandTop2 */
}

/* USER CODE BEGIN Header_StartHandBot3 */
/**
 * @brief Function implementing the HandBot3 thread.
 * @param argument: Not used
 * @retval None
 */
/* USER CODE END Header_StartHandBot3 */
void StartHandBot3(void *argument) {
	/* USER CODE BEGIN StartHandBot3 */
	bot3 = (Servo*) pvPortMalloc(sizeof(Servo));
	if (bot3 == NULL)
		Error_Handler();
	vServoInit(bot3, &htim4, TIM_CHANNEL_1, handEventHandle, FlagHandBot3);

	bot3->downLimit = 48u;
	bot3->upLimit = 75u;
	bot3->isReverse = 1u;

	vServoStart(bot3);
	/* Infinite loop */
	for (;;) {
		eServoMove(bot3);
//		if (!peakControlMode) {
//			osDelay(bot3->delay);
//		} else {
//			osDelay(bot3->delay * 4u);
//		}
//		osDelay(bot3->delay);
	}
	/* USER CODE END StartHandBot3 */
}

/* USER CODE BEGIN Header_StartHandTop3 */
/**
 * @brief Function implementing the HandTop3 thread.
 * @param argument: Not used
 * @retval None
 */
/* USER CODE END Header_StartHandTop3 */
void StartHandTop3(void *argument) {
	/* USER CODE BEGIN StartHandTop3 */
	top3 = (Servo*) pvPortMalloc(sizeof(Servo));
	if (top3 == NULL)
		Error_Handler();
	vServoInit(top3, &htim4, TIM_CHANNEL_2, handEventHandle, FlagHandTop3);

	top3->downLimit = 45u;
	top3->upLimit = 108u;
	top3->isReverse = 1u;

	vServoStart(top3);
	/* Infinite loop */
	for (;;) {
		eServoMove(top3);
//		if (!peakControlMode) {
//			osDelay(top3->delay);
//		} else {
//			osDelay(top3->delay * 4u);
//		}
//		osDelay(top3->delay);
	}
	/* USER CODE END StartHandTop3 */
}

/* USER CODE BEGIN Header_StartHandRxHandler */
/**
 * @brief Function implementing the HandRxHandler thread.
 * @param argument: Not used
 * @retval None
 */
/* USER CODE END Header_StartHandRxHandler */
void StartHandRxHandler(void *argument) {
	/* USER CODE BEGIN StartHandRxHandler */
	rxData = (uint8_t*) pvPortMalloc(NumberOfRxData * sizeof(uint8_t));
	if (rxData == NULL)
		Error_Handler();

	uint8_t *buffer = (uint8_t*) pvPortMalloc(NumberOfRxData * sizeof(uint8_t));
	if (buffer == NULL)
		Error_Handler();

	HAL_UART_Receive_DMA(&huart1, rxData, NumberOfRxData);
	/* Infinite loop */
	for (;;) {
		osEventFlagsWait(handEventHandle, FlagHandUART_RxCplt, osFlagsWaitAll,
		osWaitForever);

		memcpy(buffer, rxData, NumberOfRxData);

		//command
		if ((buffer[0] == Head) && (buffer[NumberOfRxData - 1] == Tail)) {
			countRun = 0u;
			if ((osEventFlagsGet(handEventHandle) & FlagHandPeakControlEnable)
					!= FlagHandPeakControlEnable) {
				vServoUpdateCommand(bot1, buffer, 1);
				vServoUpdateCommand(bot2, buffer, 7);
				vServoUpdateCommand(bot3, buffer, 13);

				vServoUpdateCommand(top3, buffer, 16);
				vServoUpdateCommand(top2, buffer, 10);
				vServoUpdateCommand(top1, buffer, 4);
			}

			servoLevel = buffer[19];
		}
		//setup
		else if ((buffer[0] == Tail) && (buffer[NumberOfRxData - 1] == Head)) {
			vServoUpdateSetup(bot1, buffer, 1);
			vServoUpdateSetup(bot2, buffer, 7);
			vServoUpdateSetup(bot3, buffer, 13);

			vServoUpdateSetup(top1, buffer, 4);
			vServoUpdateSetup(top2, buffer, 10);
			vServoUpdateSetup(top3, buffer, 16);

			servoLevel = buffer[19];
		}
	}
	/* USER CODE END StartHandRxHandler */
}

/* USER CODE BEGIN Header_StartHandMatrixHandler */
/**
 * @brief Function implementing the HandMatrixHandler thread.
 * @param argument: Not used
 * @retval None
 */
/* USER CODE END Header_StartHandMatrixHandler */
void StartHandMatrixHandler(void *argument) {
	/* USER CODE BEGIN StartHandMatrixHandler */
	matrix = (MyArray*) xMyArrayCreate(440u, 10u, 44u);
	if (matrix == NULL)
		Error_Handler();

	uint16_t *buf;
	osStatus_t stat;
	/* Infinite loop */
	for (;;) {
		stat = osMessageQueueGet(handQueueHandle, &buf, NULL, osWaitForever);
		if (stat == osOK) {

			for (uint16_t i = 0u; i < NumberOfCells; i++)
				matrix->data[i] = buf[i];

			vPortFree(buf);

			osEventFlagsSet(handEventHandle, FlagHandMatrix);
		}
	}
	/* USER CODE END StartHandMatrixHandler */
}

/* USER CODE BEGIN Header_StartHandPeakControl */
/**
 * @brief Function implementing the HandPeakControl thread.
 * @param argument: Not used
 * @retval None
 */
/* USER CODE END Header_StartHandPeakControl */
void StartHandPeakControl(void *argument) {
	/* USER CODE BEGIN StartHandPeakControl */
	botArray = xMyArrayCreate(120u, 5u, 24u);
	if (botArray == NULL)
		Error_Handler();
	topArray = xMyArrayCreate(120u, 5u, 24u);
	if (topArray == NULL)
		Error_Handler();
//	palmArray = xMyArrayCreate(200u, 10u, 20u);
//	if (palmArray == NULL)
//		Error_Handler();

	bot1Array = xMyArrayCreate(40u, 5u, 8u);
	if (bot1Array == NULL)
		Error_Handler();
	bot2Array = xMyArrayCreate(40u, 5u, 8u);
	if (bot2Array == NULL)
		Error_Handler();
	bot3Array = xMyArrayCreate(40u, 5u, 8u);
	if (bot3Array == NULL)
		Error_Handler();

	top1Array = xMyArrayCreate(40u, 5u, 8u);
	if (top1Array == NULL)
		Error_Handler();
	top2Array = xMyArrayCreate(40u, 5u, 8u);
	if (top2Array == NULL)
		Error_Handler();
	top3Array = xMyArrayCreate(40u, 5u, 8u);
	if (top3Array == NULL)
		Error_Handler();

	countRun = 0u;

	HAL_GPIO_WritePin(LEDGreen_GPIO_Port, LEDGreen_Pin, GPIO_PIN_SET); //off led
	osEventFlagsClear(handEventHandle, FlagHandPeakControlEnable);
	/* Infinite loop */
	for (;;) {
		osEventFlagsWait(handEventHandle, FlagHandPeakControlEnable,
		osFlagsWaitAll | osFlagsNoClear, osWaitForever);

		osEventFlagsWait(handEventHandle, FlagHandMatrix, osFlagsWaitAll,
		osWaitForever);

		eMyArraySplit(matrix, botArray, 5u, 0u);
		eMyArraySplit(matrix, topArray, 0u, 0u);
////		eMyArraySplit(matrix, palmArray, 0u, 24u);
		eMyArraySplit(botArray, bot1Array, 0u, 0u);
		eMyArraySplit(botArray, bot2Array, 0u, 8u);
		eMyArraySplit(botArray, bot3Array, 0u, 16u);

		eMyArraySplit(topArray, top1Array, 0u, 0u);
		eMyArraySplit(topArray, top2Array, 0u, 8u);
		eMyArraySplit(topArray, top3Array, 0u, 16u);
//
//		maxBot = uMyArrayFindMax(botArray);
		maxBot1 = uMyArrayFindMax(bot1Array);
		maxBot2 = uMyArrayFindMax(bot2Array);
		maxBot3 = uMyArrayFindMax(bot3Array);

		maxTop1 = uMyArrayFindMax(top1Array);
		maxTop2 = uMyArrayFindMax(top2Array);
		maxTop3 = uMyArrayFindMax(top3Array);
////		maxTop = uMyArrayFindMax(topArray);
////		maxPalm = uMyArrayFindMax(palmArray);

		threshHoldValue = threshHold[servoLevel] * 2u;
		//
		if ((flag12 == 0)
				&& (((maxBot1 + maxBot2 + 100u > threshHoldValue)
						&& (maxBot1 > threshHoldValue * 0.3f)
						&& (maxBot2 > threshHoldValue * 0.3f))
						|| ((maxTop1 + maxTop2 + 100u > threshHoldValue)
								&& (maxTop1 > threshHoldValue * 0.3f)
								&& (maxTop2 > threshHoldValue * 0.3f)))) {
			flag12 = 1;
			osEventFlagsSet(stopServoEventHandle,
			FlagStopServoBot1 | FlagStopServoBot2);
		}
		if ((flag13 == 0)
				&& (((maxBot1 + maxBot3 + 100u > threshHoldValue)
						&& (maxBot1 > threshHoldValue * 0.3f)
						&& (maxBot3 > threshHoldValue * 0.3f))
						|| ((maxTop1 + maxTop3 + 100u > threshHoldValue)
								&& (maxTop1 > threshHoldValue * 0.3f)
								&& (maxTop3 > threshHoldValue * 0.3f)))) {
			flag13 = 1;
			osEventFlagsSet(stopServoEventHandle,
			FlagStopServoBot1 | FlagStopServoBot3);
		}

		if ((flag123 == 0) && (flag12 == 1) && (flag13 == 1)) {
			flag123 = 1;
//			if (countRun < 2)
//				++countRun;
//			else {
//				countRun = 0;
//				flag123 = 1;
//			}
			volatile uint16_t temp = threshHold[servoLevel];
			//finger 1
			if (maxBot1 > temp || maxTop1 > temp)
				vServoMove1(bot1, eServoUp1);
			else if (maxBot1 < temp && maxTop1 < temp)
				vServoMove1(bot1, eServoDown1);
			//finger 2
			if (maxBot2 > temp || maxTop2 > temp)
				vServoMove1(bot2, eServoUp1);
			else if (maxBot2 < temp && maxTop2 < temp)
				vServoMove1(bot2, eServoDown1);
			//finger 3
			if (maxBot3 > temp || maxTop3 > temp)
				vServoMove1(bot3, eServoUp1);
			else if (maxBot3 < temp && maxTop3 < temp)
				vServoMove1(bot3, eServoDown1);
		}

	}
	/* USER CODE END StartHandPeakControl */
}

/* USER CODE BEGIN Header_StartButtonHandler */
/**
 * @brief Function implementing the ButtonHandler thread.
 * @param argument: Not used
 * @retval None
 */
/* USER CODE END Header_StartButtonHandler */
void StartButtonHandler(void *argument) {
	/* USER CODE BEGIN StartButtonHandler */
	/* Infinite loop */
	for (;;) {
		osThreadFlagsWait(1u, osFlagsWaitAll, osWaitForever);

		switch (btn) {
		case Btn0_Pin: //velostat enable
			if ((osEventFlagsGet(velostatEventHandle) & FlagVelostatEnable)
					== FlagVelostatEnable) { //on
				osEventFlagsClear(velostatEventHandle, FlagVelostatEnable);
			} else { //off
				osEventFlagsSet(velostatEventHandle, FlagVelostatEnable);
			}
			break;
		case Btn1_Pin:
			if ((osEventFlagsGet(velostatEventHandle) & FlagVelostatEnable)
					== FlagVelostatEnable) { //sensor on
				if ((osEventFlagsGet(handEventHandle)
						& FlagHandPeakControlEnable)
						== FlagHandPeakControlEnable) { //mode on
					osEventFlagsClear(handEventHandle,
					FlagHandPeakControlEnable);
					HAL_GPIO_WritePin(LEDGreen_GPIO_Port, LEDGreen_Pin,
							GPIO_PIN_SET); //off
					vServoMove1(bot1, eServoUp);
					vServoMove1(bot2, eServoUp);
					vServoMove1(bot3, eServoUp);
					vServoMove1(top1, eServoUp);
					vServoMove1(top2, eServoUp);
					vServoMove1(top3, eServoUp);
				} else { //mode off
					osEventFlagsSet(handEventHandle,
					FlagHandPeakControlEnable);
					HAL_GPIO_WritePin(LEDGreen_GPIO_Port, LEDGreen_Pin,
							GPIO_PIN_RESET); //on
					flag12 = 0;
					flag13 = 0;
					flag123 = 0;
					countRun = 0;
					vServoMove1(bot1, eServoDown);
					vServoMove1(bot2, eServoDown);
					vServoMove1(bot3, eServoDown);
//					vServoMove1(top1, eServoDown);
//					vServoMove1(top2, eServoDown);
//					vServoMove1(top3, eServoDown);
				}
			}
			break;
		}

		if (isPress) {
			isPress = 0u;
			osTimerStart(myTimer01Handle, 1000u);
		}
	}
	/* USER CODE END StartButtonHandler */
}

/* USER CODE BEGIN Header_StartHandStopServo */
/**
 * @brief Function implementing the HandStopServo thread.
 * @param argument: Not used
 * @retval None
 */
/* USER CODE END Header_StartHandStopServo */
void StartHandStopServo(void *argument) {
	/* USER CODE BEGIN StartHandStopServo */
	/* Infinite loop */
	for (;;) {
		osEventFlagsWait(stopServoEventHandle,
				FlagStopServoBot1 | FlagStopServoBot2 | FlagStopServoBot3
						| FlagStopServoTop1 | FlagStopServoTop2
						| FlagStopServoTop3, osFlagsWaitAny | osFlagsNoClear,
				osWaitForever);

		flag = osEventFlagsGet(stopServoEventHandle);
		if ((flag & FlagStopServoBot1) == FlagStopServoBot1) {
			osEventFlagsClear(stopServoEventHandle, FlagStopServoBot1);
			bot1->command = eServoStop;
			osEventFlagsClear(handEventHandle, FlagHandBot1);
		}

		if ((flag & FlagStopServoBot2) == FlagStopServoBot2) {
			osEventFlagsClear(stopServoEventHandle, FlagStopServoBot2);
			bot2->command = eServoStop;
			osEventFlagsClear(handEventHandle, FlagHandBot2);
		}

		if ((flag & FlagStopServoBot3) == FlagStopServoBot3) {
			osEventFlagsClear(stopServoEventHandle, FlagStopServoBot3);
			bot3->command = eServoStop;
			osEventFlagsClear(handEventHandle, FlagHandBot3);
		}

		if ((flag & FlagStopServoTop1) == FlagStopServoTop1) {
			osEventFlagsClear(stopServoEventHandle, FlagStopServoTop1);
			top1->command = eServoStop;
			osEventFlagsClear(handEventHandle, FlagHandTop1);
		}

		if ((flag & FlagStopServoTop2) == FlagStopServoTop2) {
			osEventFlagsClear(stopServoEventHandle, FlagStopServoTop2);
			top2->command = eServoStop;
			osEventFlagsClear(handEventHandle, FlagHandTop2);
		}

		if ((flag & FlagStopServoTop3) == FlagStopServoTop3) {
			osEventFlagsClear(stopServoEventHandle, FlagStopServoTop3);
			top3->command = eServoStop;
			osEventFlagsClear(handEventHandle, FlagHandTop3);
		}

	}
	/* USER CODE END StartHandStopServo */
}

/* Callback01 function */
void Callback01(void *argument) {
	/* USER CODE BEGIN Callback01 */
	flagTimer = 1u;
	/* USER CODE END Callback01 */
}

/**
 * @brief  Period elapsed callback in non blocking mode
 * @note   This function is called  when TIM1 interrupt took place, inside
 * HAL_TIM_IRQHandler(). It makes a direct call to HAL_IncTick() to increment
 * a global variable "uwTick" used as application time base.
 * @param  htim : TIM handle
 * @retval None
 */
void HAL_TIM_PeriodElapsedCallback(TIM_HandleTypeDef *htim) {
	/* USER CODE BEGIN Callback 0 */

	/* USER CODE END Callback 0 */
	if (htim->Instance == TIM1) {
		HAL_IncTick();
	}
	/* USER CODE BEGIN Callback 1 */

	/* USER CODE END Callback 1 */
}

/**
 * @brief  This function is executed in case of error occurrence.
 * @retval None
 */
void Error_Handler(void) {
	/* USER CODE BEGIN Error_Handler_Debug */
	/* User can add his own implementation to report the HAL error return state */
	__disable_irq();
	while (1) {
	}
	/* USER CODE END Error_Handler_Debug */
}

#ifdef  USE_FULL_ASSERT
/**
  * @brief  Reports the name of the source file and the source line number
  *         where the assert_param error has occurred.
  * @param  file: pointer to the source file name
  * @param  line: assert_param error line source number
  * @retval None
  */
void assert_failed(uint8_t *file, uint32_t line)
{
  /* USER CODE BEGIN 6 */
  /* User can add his own implementation to report the file name and line number,
     ex: printf("Wrong parameters value: file %s on line %d\r\n", file, line) */
  /* USER CODE END 6 */
}
#endif /* USE_FULL_ASSERT */

/************************ (C) COPYRIGHT STMicroelectronics *****END OF FILE****/
