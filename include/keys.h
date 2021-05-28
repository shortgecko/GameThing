#pragma once
//Copied from https://github.com/NoelFB/blah/blob/e55e84f14906caf7e512d7904a7ab467ec985fa6/include/blah/input/input.h
//notes:
// These are generally copied from the SDL2 Scancode Keys,
// which are in turn based on the USB standards:
// https://www.usb.org/sites/default/files/documents/hut1_12v2.pdf

#define BLAH_KEY_DEFINITIONS \
	DEFINE_KEY(Unknown, 0) \
	\
	DEFINE_KEY(A, 4) \
	DEFINE_KEY(B, 5) \
	DEFINE_KEY(C, 6) \
	DEFINE_KEY(D, 7) \
	DEFINE_KEY(E, 8) \
	DEFINE_KEY(F, 9) \
	DEFINE_KEY(G, 10) \
	DEFINE_KEY(H, 11) \
	DEFINE_KEY(I, 12) \
	DEFINE_KEY(J, 13) \
	DEFINE_KEY(K, 14) \
	DEFINE_KEY(L, 15) \
	DEFINE_KEY(M, 16) \
	DEFINE_KEY(N, 17) \
	DEFINE_KEY(O, 18) \
	DEFINE_KEY(P, 19) \
	DEFINE_KEY(Q, 20) \
	DEFINE_KEY(R, 21) \
	DEFINE_KEY(S, 22) \
	DEFINE_KEY(T, 23) \
	DEFINE_KEY(U, 24) \
	DEFINE_KEY(V, 25) \
	DEFINE_KEY(W, 26) \
	DEFINE_KEY(X, 27) \
	DEFINE_KEY(Y, 28) \
	DEFINE_KEY(Z, 29) \
	\
	DEFINE_KEY(D1, 30) \
	DEFINE_KEY(D2, 31) \
	DEFINE_KEY(D3, 32) \
	DEFINE_KEY(D4, 33) \
	DEFINE_KEY(D5, 34) \
	DEFINE_KEY(D6, 35) \
	DEFINE_KEY(D7, 36) \
	DEFINE_KEY(D8, 37) \
	DEFINE_KEY(D9, 38) \
	DEFINE_KEY(D0, 39) \
	\
	DEFINE_KEY(Enter, 40) \
	DEFINE_KEY(Escape, 41) \
	DEFINE_KEY(Backspace, 42) \
	DEFINE_KEY(Tab, 43) \
	DEFINE_KEY(Space, 44) \
	\
	DEFINE_KEY(Minus, 45) \
	DEFINE_KEY(Equals, 46) \
	DEFINE_KEY(LeftBracket, 47) \
	DEFINE_KEY(RightBracket, 48) \
	DEFINE_KEY(Backslash, 49) \
	DEFINE_KEY(Semicolon, 51) \
	DEFINE_KEY(Apostrophe, 52) \
	DEFINE_KEY(Tilde, 53) \
	DEFINE_KEY(Comma, 54) \
	DEFINE_KEY(Period, 55) \
	DEFINE_KEY(Slash, 56) \
	\
	DEFINE_KEY(Capslock, 57) \
	\
	DEFINE_KEY(F1, 58) \
	DEFINE_KEY(F2, 59) \
	DEFINE_KEY(F3, 60) \
	DEFINE_KEY(F4, 61) \
	DEFINE_KEY(F5, 62) \
	DEFINE_KEY(F6, 63) \
	DEFINE_KEY(F7, 64) \
	DEFINE_KEY(F8, 65) \
	DEFINE_KEY(F9, 66) \
	DEFINE_KEY(F10, 67) \
	DEFINE_KEY(F11, 68) \
	DEFINE_KEY(F12, 69) \
	DEFINE_KEY(F13, 104) \
	DEFINE_KEY(F14, 105) \
	DEFINE_KEY(F15, 106) \
	DEFINE_KEY(F16, 107) \
	DEFINE_KEY(F17, 108) \
	DEFINE_KEY(F18, 109) \
	DEFINE_KEY(F19, 110) \
	DEFINE_KEY(F20, 111) \
	DEFINE_KEY(F21, 112) \
	DEFINE_KEY(F22, 113) \
	DEFINE_KEY(F23, 114) \
	DEFINE_KEY(F24, 115) \
	\
	DEFINE_KEY(PrintScreen, 70) \
	DEFINE_KEY(ScrollLock, 71) \
	DEFINE_KEY(Pause, 72) \
	DEFINE_KEY(Insert, 73) \
	DEFINE_KEY(Home, 74) \
	DEFINE_KEY(PageUp, 75) \
	DEFINE_KEY(Delete, 76) \
	DEFINE_KEY(End, 77) \
	DEFINE_KEY(PageDown, 78) \
	DEFINE_KEY(Right, 79) \
	DEFINE_KEY(Left, 80) \
	DEFINE_KEY(Down, 81) \
	DEFINE_KEY(Up, 82) \
	\
	DEFINE_KEY(Numlock, 83) \
	\
	DEFINE_KEY(Application, 101) \
	\
	DEFINE_KEY(Execute, 116) \
	DEFINE_KEY(Help, 117) \
	DEFINE_KEY(Menu, 118) \
	DEFINE_KEY(Select, 119) \
	DEFINE_KEY(Stop, 120) \
	DEFINE_KEY(Redo, 121) \
	DEFINE_KEY(Undo, 122) \
	DEFINE_KEY(Cut, 123) \
	DEFINE_KEY(Copy, 124) \
	DEFINE_KEY(Paste, 125) \
	DEFINE_KEY(Find, 126) \
	DEFINE_KEY(Mute, 127) \
	DEFINE_KEY(VolumeUp, 128) \
	DEFINE_KEY(VolumeDown, 129) \
	\
	DEFINE_KEY(AltErase, 153) \
	DEFINE_KEY(SysReq, 154) \
	DEFINE_KEY(Cancel, 155) \
	DEFINE_KEY(Clear, 156) \
	DEFINE_KEY(Prior, 157) \
	DEFINE_KEY(Enter2, 158) \
	DEFINE_KEY(Separator, 159) \
	DEFINE_KEY(Out, 160) \
	DEFINE_KEY(Oper, 161) \
	DEFINE_KEY(ClearAgain, 162) \
	\
	DEFINE_KEY(KeypadA, 188) \
	DEFINE_KEY(KeypadB, 189) \
	DEFINE_KEY(KeypadC, 190) \
	DEFINE_KEY(KeypadD, 191) \
	DEFINE_KEY(KeypadE, 192) \
	DEFINE_KEY(KeypadF, 193) \
	DEFINE_KEY(Keypad0, 98) \
	DEFINE_KEY(Keypad00, 176) \
	DEFINE_KEY(Keypad000, 177) \
	DEFINE_KEY(Keypad1, 89) \
	DEFINE_KEY(Keypad2, 90) \
	DEFINE_KEY(Keypad3, 91) \
	DEFINE_KEY(Keypad4, 92) \
	DEFINE_KEY(Keypad5, 93) \
	DEFINE_KEY(Keypad6, 94) \
	DEFINE_KEY(Keypad7, 95) \
	DEFINE_KEY(Keypad8, 96) \
	DEFINE_KEY(Keypad9, 97) \
	DEFINE_KEY(KeypadDivide, 84) \
	DEFINE_KEY(KeypadMultiply, 85) \
	DEFINE_KEY(KeypadMinus, 86) \
	DEFINE_KEY(KeypadPlus, 87) \
	DEFINE_KEY(KeypadEnter, 88) \
	DEFINE_KEY(KeypadPeroid, 99) \
	DEFINE_KEY(KeypadEquals, 103) \
	DEFINE_KEY(KeypadComma, 133) \
	DEFINE_KEY(KeypadLeftParen, 182) \
	DEFINE_KEY(KeypadRightParen, 183) \
	DEFINE_KEY(KeypadLeftBrace, 184) \
	DEFINE_KEY(KeypadRightBrace, 185) \
	DEFINE_KEY(KeypadTab, 186) \
	DEFINE_KEY(KeypadBackspace, 187) \
	DEFINE_KEY(KeypadXor, 194) \
	DEFINE_KEY(KeypadPower, 195) \
	DEFINE_KEY(KeypadPercent, 196) \
	DEFINE_KEY(KeypadLess, 197) \
	DEFINE_KEY(KeypadGreater, 198) \
	DEFINE_KEY(KeypadAmpersand, 199) \
	DEFINE_KEY(KeypadColon, 203) \
	DEFINE_KEY(KeypadHash, 204) \
	DEFINE_KEY(KeypadSpace, 205) \
	DEFINE_KEY(KeypadClear, 216) \
	\
	DEFINE_KEY(LeftControl, 224) \
	DEFINE_KEY(LeftShift, 225) \
	DEFINE_KEY(LeftAlt, 226) \
	DEFINE_KEY(LeftOS, 227) \
	DEFINE_KEY(RightControl, 228) \
	DEFINE_KEY(RightShift, 229) \
	DEFINE_KEY(RightAlt, 230) \
	DEFINE_KEY(RightOS, 231)


#define BLAH_BUTTON_DEFINITIONS \
	DEFINE_BTN(None, 1) \
	DEFINE_BTN(A, 0) \
	DEFINE_BTN(B, 1) \
	DEFINE_BTN(X, 2) \
	DEFINE_BTN(Y, 3) \
	DEFINE_BTN(Back, 4) \
	DEFINE_BTN(Select, 5) \
	DEFINE_BTN(Start, 6) \
	DEFINE_BTN(LeftStick, 7) \
	DEFINE_BTN(RightStick, 8) \
	DEFINE_BTN(LeftShoulder, 9) \
	DEFINE_BTN(RightShoulder, 10) \
	DEFINE_BTN(Up, 11) \
	DEFINE_BTN(Down, 12) \
	DEFINE_BTN(Left, 13) \
	DEFINE_BTN(Right, 14)

enum class Keys
{
#define DEFINE_KEY(name, value) name = value,
	BLAH_KEY_DEFINITIONS
#undef DEFINE_KEY
};

// Game Controller Buttons
enum class Button
{
#define DEFINE_BTN(name, value) name = value,
	BLAH_BUTTON_DEFINITIONS
#undef DEFINE_BTN
};