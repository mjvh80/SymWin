SymWin ♠
========

Allows one to type special characters such as å or λ on Windows without the alt-key bullshit or frequent language switching which is just cumbersome.

Inspired by Apple's iOS and OS X keyboard symbol selection, this shows a popup with symbols wherever you are typing.

It looks like this:

![screenshot](SymWin/screencast.gif)

To type a symbol hold down CAPSLOCK and then (say) the *a* key. This will show the above popup.

To type an uppercase symbol, simply add SHIFT to the above key sequence.

To cycle options keep pressing the letter key, or use the arrow keys on the keyboard. You can also use the mouse to select a symbol. To go backwards, hold the ALT key in addition to the other keys.

This is just an initial rough version which is working well. It works particularly well for Microsoft applications which use the Win32 Caret api (SetCaretPos in particular), other applications work but without positioning of the popup at the caret. Instead, the centre of the screen is chosen for these applications.

It is possible to configure one's symbol settings, these will be saved on a per-user basis (see tasktray menu).

It is also possible to temporarily disable SymWin if you need access to CAPSLOCK for instance (see tasktray menu).

How To
------
Currently I do not provide a pre-built version, but may do so given sufficient interest.

It is easy to build, however, my running the ```build.cmd``` script or using Visual Studio. The steps are:

- clone this repository
- run build.cmd or use Visual Studio

Todo:
-----
- SymWin does not activate the popup window when the keyboard is used which makes the application appear within the application the user is currently working in. This does *not*, however, work when the mouse is used (the target application loses focus for a second). I don't currently know how to prevent this. Any help is appreciated.


Nice Features Perhaps:
----------------------

- figure out the best way to run in the background with enough privileges to avoid UAC popups or run into UIPI windows issues - this isn't required to use this tool but it'd make it more generally available (and we could create an installer etc)
- make default symbol mappings depend on the current active keyboard, e.g. a Swedish keyboard doesn't need the å symbol for instance
- make "hot key" (currently CAPSLOCK) configurable
- (not sure) dynamically re-order symbols based on usage patterns (e.g. more frequently used symbols are faster to get to)

If you're interested in any of these features feel free to implement them and open a pull request. Otherwise talk to me, I might just be persuaded to implement the feature.

Metro
-----
SymWin does not currently show in Metro because all kinds of special stuff needs to happen:
- The window requires WS_EX_TOPMOST a flag which WPF does not set if ShowInTaskbar = false, but can be set using SetWindowPos.
- The application must have uiaccess="true" for which it must be signed using a certificate which must be installed to the Trusted Root Certificate Authority.
- It must be run from a trusted location, e.g. Prog Files

Is such support realistically possible without being Microsoft?

See http://blogs.microsoft.co.il/pavely/2012/05/16/windows-8-topmost-vs-topmost/ for details.


Finally
-------
I know CAPSLOCK may not appease everyone and my app currently disables it. I never use CAPSLOCK for anything meaningful and it is one of the few key combinations that I could think off that is not already used and is easy to type.
