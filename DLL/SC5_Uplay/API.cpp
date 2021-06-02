#include "API.h"
#include <stdlib.h> 
#include <cstring>

u8* BuildMsg(u8 type, void* data, int len)
{
	u8* temp = new u8[len + 1];
	temp[0] = type;
	if ((data != nullptr) && (len > 0))
		std::memcpy(&temp[1], data, len);
	return temp;
}

void ParseMsg(u8* msg)
{
	u8 type = msg[0];
	switch (type)
	{
	default:
		break;
	}
}