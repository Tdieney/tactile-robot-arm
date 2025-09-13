/*
 * MyArray.h
 *
 *  Created on: Jun 1, 2022
 *      Author: HoangXuan
 */

#ifndef MYARRAY_H_
#define MYARRAY_H_

#include "cmsis_os.h"
#include <stdlib.h>
#include <stdint.h>
#include <stddef.h>

typedef enum {
	eMyArrayOK, eMyArrayLenError, eMyArrayPositionError, eMyArrayNullData
} eMyArray;

typedef struct {
	uint16_t *data;
	uint32_t length;
	uint32_t width;
	uint32_t height;
} MyArray;

MyArray* xMyArrayCreate(uint32_t len, uint32_t w, uint32_t h);
eMyArray eMyArrayCheck(MyArray *arr);
eMyArray eMyArraySplit(const MyArray *src, MyArray *dst, uint32_t x0,
		uint32_t y0);
uint16_t uMyArrayFindMax(const MyArray *arr);

#endif /* MYARRAY_H_ */
