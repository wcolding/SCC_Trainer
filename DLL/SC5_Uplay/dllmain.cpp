// dllmain.cpp : Defines the entry point for the DLL application.
#include "pch.h"
#include "API.h"
#include <Windows.h>

#define SendClient(a,b,c) WriteFile(outPipe, BuildMsg(a,b,c), c+1, bytesIO, 0);

DWORD WINAPI TrainerThread(LPVOID lpParam)
{
    //MessageBox(NULL, (LPCTSTR)"This is injected!", (LPCTSTR)"INJECTION", MB_OK);

    HANDLE outPipe = INVALID_HANDLE_VALUE;
    HANDLE inPipe = INVALID_HANDLE_VALUE;
    LPDWORD bytesIO = 0;

    // Find pipes before doing anything else
    while (true)
    {
        outPipe = CreateFile(PIPE_TO_APP, GENERIC_WRITE, 0, NULL, OPEN_EXISTING, 0, NULL);
        inPipe = CreateFile(PIPE_FROM_APP, GENERIC_READ, 0, NULL, OPEN_EXISTING, 0, NULL);

        if ((outPipe != INVALID_HANDLE_VALUE) && (inPipe != INVALID_HANDLE_VALUE))
            break;
    }


    while (true)
    {
        SendClient(MSG_SUCCESS, nullptr, 0);
        Sleep(1000);
    }

    return 0;
}


BOOL APIENTRY DllMain( HMODULE hModule, DWORD  ul_reason_for_call, LPVOID lpReserved)
{
    switch (ul_reason_for_call)
    {
        case DLL_PROCESS_ATTACH:
        {
            //MessageBox(NULL, TEXT("Process Attached!"), TEXT("INJECTION"), MB_OK);
            // Start trainer thread
            DWORD trainerThreadID = 0;
            CreateThread(
                NULL,                   // default security attributes
                0,                      // use default stack size  
                TrainerThread,       // thread function name
                0,               // argument to thread function 
                0,                      // use default creation flags 
                &trainerThreadID);
            break;
        }
        case DLL_THREAD_ATTACH:
            //MessageBox(NULL, TEXT("Thread Attached!"), TEXT("INJECTION"), MB_OK);
            break;
        case DLL_THREAD_DETACH:
            break;
        case DLL_PROCESS_DETACH:
            break;
    }
    return TRUE;
}



