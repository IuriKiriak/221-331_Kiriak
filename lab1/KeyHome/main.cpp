#include <fstream>
#include <vector>
#include <windows.h>
#include <bcrypt.h>
#include <iostream>
#pragma comment(lib, "bcrypt.lib")

// для справки https://anti-debug.checkpoint.com/techniques/interactive.html#self-debugging

bool CheckFileSHA256(const std::string& path, const std::string& referenceHashHex)
{
    std::ifstream file(path, std::ios::binary);
    if (!file) return false;

    std::vector<unsigned char> buffer((std::istreambuf_iterator<char>(file)),
        std::istreambuf_iterator<char>());

    BCRYPT_ALG_HANDLE hAlg = nullptr;
    if (BCryptOpenAlgorithmProvider(&hAlg, BCRYPT_SHA256_ALGORITHM, nullptr, 0) != 0)
        return false;

    unsigned char hash[32];
    if (BCryptHash(hAlg, nullptr, 0, buffer.data(), static_cast<ULONG>(buffer.size()), hash, sizeof(hash)) != 0)
    {
        BCryptCloseAlgorithmProvider(hAlg, 0);
        return false;
    }

    BCryptCloseAlgorithmProvider(hAlg, 0);

    std::string hashHex;
    for (int i = 0; i < 32; ++i)
    {
        char buf[3];
        sprintf_s(buf, "%02x", hash[i]);
        hashHex += buf;
    }
    std::cout << hashHex << std::endl;

    return hashHex == referenceHashHex;
}

int main()
{
    if (CheckFileSHA256("D:/Data/Desctop/test/KeyHome.exe", "644a44a9aa93692f354f70be4795c1c474e0dff262d8b7a0c020cb8f9af1b41d"))
    {
        std::cout << "access" << std::endl;
    }
    else
    {
        std::cout << "failed" << std::endl;
    }
    // 1. Запустить password manager и получить его PID
    // https://learn.microsoft.com/en-us/windows/win32/api/processthreadsapi/nf-processthreadsapi-openprocess
       

    STARTUPINFO si;
    PROCESS_INFORMATION pi;
    ZeroMemory(&si, sizeof(si));
    si.cb = sizeof(si);
    ZeroMemory(&pi, sizeof(pi));


    wchar_t cmdLine[] = L"\"D:/Data/Desctop/test/KeyHome.exe\"";


    if (CreateProcess(
        NULL, cmdLine,
        NULL, NULL,
        TRUE, NULL,
        NULL, NULL,
        &si, &pi)) {
        std::cout << "***CreateProcessW() success!" << std::endl;
        std::cout << "***CreateProcessW() pid = " << std::dec << pi.dwProcessId << std::endl;
    }
    else {
        std::cout << "***CreateProcessW() FAILED" << std::endl;
    }




    // 2. Подключиться к процессу как отладчик
    // https://learn.microsoft.com/en-us/windows/win32/debug/writing-the-debugger-s-main-loop


    bool isAttached = DebugActiveProcess(pi.dwProcessId);
    if (!isAttached) {// проверка, удалось ли подключиться
        DWORD lastError = GetLastError();
        std::cout << std::hex << "***DebugActiveProcess() FAILED, GetLastError() = " << lastError;
    }
    else {
        std::cout << "***DebugActiveProcess() success!" << std::endl;
    }


    // 3. Пропускать поступающие сигналы отладки


    DEBUG_EVENT debugEvent;
    while (true) {
        bool result1 = WaitForDebugEvent(&debugEvent, INFINITE);
        bool result2 = ContinueDebugEvent(debugEvent.dwProcessId,
            debugEvent.dwThreadId,
            DBG_CONTINUE);
    }

    return 0;
}
