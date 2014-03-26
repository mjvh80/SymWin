SymWin
======

Allows one to type special characters such as å or λ on Windows without the alt-key bullshit.

Inspired by Apple's iOS keyboard symbol selection, this shows a popup with symbols wherever you are typing.

It looks like this:

![screenshot](SymWin/screenshot.png)

To type a symbol type CAPSLOCK and then (say) the a key. This will show the above popup.

To type an uppercase symbol, simply add SHIFT to the above keys.

To cycle options keep pressing the letter key, or use the arrow keys on the keyboard.

Finally you can also use the mouse to select a symbol.

This is just an initial rough version which seems to be working.

Things I'd like to do:

- dogfooding / bug fixing / remove rough edges
- the code can be optimized
- how to deal with various cultures
- add tasktray support
- add support for configuring symbols (via tasktray settings menu)

Things that may be nice but I'm not sure about:

- dynamically re-order symbols based on usage patterns (e.g. more frequently used symobls are faster to get to)
