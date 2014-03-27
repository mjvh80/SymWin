SymWin
======

Allows one to type special characters such as å or λ on Windows without the alt-key bullshit.

Inspired by Apple's iOS keyboard symbol selection, this shows a popup with symbols wherever you are typing.

It looks like this:

![screenshot](SymWin/screenshot.png)

To type a symbol hold down CAPSLOCK and then (say) the *a* key. This will show the above popup.

To type an uppercase symbol, simply add SHIFT to the above key sequence.

To cycle options keep pressing the letter key, or use the arrow keys on the keyboard. You can also use the mouse to select a symbol.

This is just an initial rough version which is working well in most applications.

Things I'd like to do:
----------------------

- dogfooding / bug fixing / remove rough edges
- the code can be optimized
- make "hot key" (currently CAPSLOCK) configurable
- how to deal with various cultures
- deal with popup showing outside of screen boundaries
- investigate why it does not work in Visual Studio
- add tasktray support
- add support for configuring symbols (via tasktray settings menu)

Things that may be nice but I'm not sure about:
-----------------------------------------------

- dynamically re-order symbols based on usage patterns (e.g. more frequently used symobls are faster to get to)

Finally
-------
I know CAPSLOCK may not appease everyone and my app currently disables it. I never use CAPSLOCK for anything meaningful and it is one of the few key combinations that I could think off that is not already used and is easy to type.
