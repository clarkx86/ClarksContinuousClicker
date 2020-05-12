<html>
    <body>
        <div align="center">
            <h1>Clarks Continuous Clicker</h1>
            <a href="https://travis-ci.com/github/clarkx86/ClarksContinuousClicker"><img alt="Build Status" src="https://travis-ci.com/clarkx86/ClarksContinuousClicker.svg?branch=master"></a>
            <a href="https://github.com/clarkx86/ClarksContinuousClicker/releases/latest"><img alt="GitHub All Releases" src="https://img.shields.io/github/downloads/clarkx86/clarkscontinuousclicker/total?label=Download&logo=Windows"></a>
            <br><br>
        </div>
    </body>
</html>
This is a simple and open-source auto-clicker command-line tool for Windows that can simulate virtual mouse events on an interval.

## Parameters
```
PARAMETER             VALUE           ABOUT
----------------------------------------------------------
-b | --mouse-button   String (!)      The mouse button to simulate.
                                      Valid values are:
                                        left, middle, right
                                      for each mouse-button respectively.

-i | --interval       Double (!)      Interval in milliseconds to perform the virtual mouse event.

-x                    Signed Integer  (Optional) X-coordinate to place mouse cursor.

-y                    Signed Integer  (Optional) Y-coordinate to place mouse cursor.

-h | --help                           Prints an overview of available parameters.

-v                                    Enable verbosity.
```

## Usage example
```
.\ccc.exe -b right -i 3000
```
... to perform a **right-click** every **3** seconds.

## Download
You can grab the latest pre-built release [**here**](https://github.com/clarkx86/ClarksContinuousClicker/releases/latest)