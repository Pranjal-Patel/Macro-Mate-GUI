import pyautogui
import time
from sys import argv

ARG = argv

def main():
    file_path = ARG[1]
    with open(file_path, "r") as file:
        file_contents = file.read().split("\n")
        for index, key in enumerate(file_contents):
            try:
                if key.startswith('$DELAY'):
                    args = key.split()
                    if len(args) == 2:
                        time.sleep(float(args[1]))
                    else:
                        print("Syntax error on line '{}'".format(index+1))
                        exit(1)
                    continue

                elif key.startswith('$'):
                    pyautogui.press(key.replace('$', ''))

                elif key.startswith('!'):
                    args = key.replace('!', '').split()
                    hotKeyArgsLength = len(args)

                    if hotKeyArgsLength == 2:
                        with pyautogui.hold(args[0]):
                            pyautogui.press(args[1])
                else:
                    pyautogui.typewrite(key.replace('$', ''))
            except:
                pass
        exit(0)

if __name__ == "__main__":
    main()
