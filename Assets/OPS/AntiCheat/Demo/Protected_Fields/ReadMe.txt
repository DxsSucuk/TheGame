Protected Fields:
--------------------

Most of your data, for example positions or health, is stored in the runtime memory.
This memory is easy accessible by cheat tools or data sniffer.
To prevent unwanted modification of the data in the memory, you have to encrypt it.
To do so, you can use AntiCheats protected fields, to protect your application runtime data
against cheater.

Using the protected fields is straightforward:
1) Create a GameObject and attach the OPS.AntiCheat.Detector.FieldCheatDetector Component.

2) Include the namespace OPS.AntiCheat.Field in your scripts - Here you find all the protected field types.

3) Replace your unprotected field types with the protected one.
Example: int to ProtectedInt32

4) More examples can be found in the ProtectedFieldsDemo class.

5) To get a callback if a cheater got detected, attach to the OPS.AntiCheat.Detector.FieldCheatDetector.OnFieldCheatDetected event.
