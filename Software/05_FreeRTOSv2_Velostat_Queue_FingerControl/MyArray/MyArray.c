/*
 * MyArray.c
 *
 *  Created on: Jun 1, 2022
 *      Author: HoangXuan
 */

#include "MyArray.h"

MyArray* xMyArrayCreate(uint32_t len, uint32_t w, uint32_t h) {
	MyArray *arr = (MyArray*) pvPortMalloc(sizeof(MyArray));
	if (arr == NULL)
		return NULL;
	arr->data = (uint16_t*) pvPortMalloc(len * sizeof(uint16_t));
	if (arr->data == NULL)
		return NULL;

	arr->length = len;
	arr->width = w;
	arr->height = h;

	return arr;
}

void vMyArrayDelete(MyArray *arr) {
	vPortFree(arr->data);
	vPortFree(arr);
}

eMyArray eMyArraySplit(const MyArray *src, MyArray *dst, uint32_t x0,
		uint32_t y0) {
	uint32_t h, w, dstH, dstW, srcH, srcW;
	dstH = dst->height;
	dstW = dst->width;
	srcH = src->height;
	srcW = src->width;

	if (x0 + dstW > srcW || y0 + dstH > srcH)
		return eMyArrayPositionError;

	for (h = 0u; h < dstH; h++) {
		for (w = 0u; w < dstW; w++) {
			dst->data[h * dstW + w] = src->data[((h + y0) * srcW) + w + x0];
		}
	}

	return eMyArrayOK;
}

uint16_t uMyArrayFindMax(const MyArray *arr) {
	uint16_t max = 0u, temp;

	for (uint32_t i = 0; i < arr->length; i++) {
		temp = arr->data[i];
		if (temp > max)
			max = temp;
	}

	return max;
}
