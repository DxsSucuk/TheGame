Protected PlayerPrefs:
--------------------

Unitys PlayerPrefs are useful to store user settings and user data.
Unfortunately these are not protected or encrypted and can be easily modified.
To prevent this, you can use the AntiCheat Protected PlayerPrefs.

Using the protected player prefs is straightforward:
1) Include the namespace: OPS.AntiCheat.Prefs

2) Here you find 2 classes.
-> ProtectedPlayerPrefs: Replaces the default functions of the Unity PlayerPrefs and adds some new. The protected prefs will be stored at the know default location.

-> ProtectedFileBasedPlayerPrefs: Is a custom implementation of the Unity PlayerPrefs allowing to store the player prefs protected at a custom file path.
   To assign a custom file path, set ProtectedFileBasedPlayerPrefs.FilePath. Now use ProtectedFileBasedPlayerPrefs like you would use the default Unity PlayerPrefs.
   
3) For some more examples, have a look at the ProtectedPlayerPrefsDemo class.