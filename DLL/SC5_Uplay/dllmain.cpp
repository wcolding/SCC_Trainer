// dllmain.cpp : Defines the entry point for the DLL application.
#include "pch.h"
#include "API.h"
#include <Windows.h>

#define SendClient(a,b,c) WriteFile(outPipe, BuildMsg(a,b,c), c+1, bytesIO, 0);

bool Hook(void* original, void* detour, int len)
{
    if (len < 5)
        return false;

    DWORD defaultProtection;
    VirtualProtect(original, len, PAGE_EXECUTE_READWRITE, &defaultProtection);
    memset(original, 0x90, len);

    DWORD jmpOffset = (DWORD)detour - (DWORD)original - 5;
    *(BYTE*)original = 0xE9;
    *(DWORD*)((DWORD)original + 1) = jmpOffset;

    DWORD output;
    VirtualProtect(original, len, defaultProtection, &output);

    return true;
}

DWORD BaseModule = (DWORD)GetModuleHandleA("Conviction_game.exe");

/////////////////
// ESAM SECTION
DWORD ESam = 0;
DWORD ESam_New = 0;
DWORD ESamHookStart  = BaseModule + 0x39B25F;
DWORD ESamHookReturn = BaseModule + 0x39B267;

void __declspec(naked) GetESam()
{
    __asm 
    {
        mov [ESam_New], eax

        // Original code
        movss xmm0, [eax+0x0000009C]
        jmp [ESamHookReturn]
    }
}
//////////////////

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

    SendClient(MSG_SUCCESS, nullptr, 0);

    // Assign function hooks
    Hook((void*)ESamHookStart, GetESam, 8);

    // Main loop
    while (true)
    {
        if (ESam != ESam_New)
        {
            ESam = ESam_New;
            SendClient(UPDATE_ESAM, &ESam, 4);
        }

        Sleep(10);
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



