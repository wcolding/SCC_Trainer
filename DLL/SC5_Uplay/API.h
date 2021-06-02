#pragma once

#define PIPE_TO_APP   L"\\\\.\\pipe\\toApp"
#define PIPE_FROM_APP L"\\\\.\\pipe\\fromApp"
#define PIPE_BUFFER_SIZE 256

#define MSG_SUCCESS 1
#define MSG_FAILURE 2

#define UPDATE_ESAM 3

typedef unsigned __int8 u8;

// Returns a char array to be sent along the pipe
//   'type' is the header
//   'data' is the actual information being passed
//   'len' is the length of data to copy
u8* BuildMsg(u8 type, void* data, int len);

void ParseMsg(u8* msg);