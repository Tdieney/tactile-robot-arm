################################################################################
# Automatically-generated file. Do not edit!
# Toolchain: GNU Tools for STM32 (9-2020-q2-update)
################################################################################

# Add inputs and outputs from these tool invocations to the build variables 
C_SRCS += \
../Scan_74238/Scan_74238.c 

OBJS += \
./Scan_74238/Scan_74238.o 

C_DEPS += \
./Scan_74238/Scan_74238.d 


# Each subdirectory must supply rules for building sources it contributes
Scan_74238/Scan_74238.o: ../Scan_74238/Scan_74238.c Scan_74238/subdir.mk
	arm-none-eabi-gcc "$<" -mcpu=cortex-m3 -std=gnu11 -g3 -DDEBUG -DUSE_HAL_DRIVER -DSTM32F103xB -c -I../Core/Inc -I../Drivers/STM32F1xx_HAL_Driver/Inc -I../Drivers/STM32F1xx_HAL_Driver/Inc/Legacy -I../Middlewares/Third_Party/FreeRTOS/Source/include -I../Middlewares/Third_Party/FreeRTOS/Source/CMSIS_RTOS_V2 -I../Middlewares/Third_Party/FreeRTOS/Source/portable/GCC/ARM_CM3 -I../Drivers/CMSIS/Device/ST/STM32F1xx/Include -I../Drivers/CMSIS/Include -I"E:/GraduateProject/STM32/MyProjects/05_FreeRTOSv2_Velostat_Queue_FingerControl/Scan_74238" -I"E:/GraduateProject/STM32/MyProjects/05_FreeRTOSv2_Velostat_Queue_FingerControl/Servo/Inc" -I"E:/GraduateProject/STM32/MyProjects/05_FreeRTOSv2_Velostat_Queue_FingerControl/MyArray" -Ofast -fdata-sections -Wall -fstack-usage -MMD -MP -MF"Scan_74238/Scan_74238.d" -MT"$@" --specs=nano.specs -mfloat-abi=soft -mthumb -o "$@"

