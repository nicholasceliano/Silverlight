﻿function onSilverlightError(t, e) { var i = ""; null != t && 0 != t && (i = t.getHost().Source); var n = e.ErrorType, s = e.ErrorCode; if ("ImageError" != n && "MediaError" != n) { var a = "Unhandled Error in Silverlight Application " + i + "\n"; throw a += "Code: " + s + "    \n", a += "Category: " + n + "       \n", a += "Message: " + e.ErrorMessage + "     \n", "ParserError" == n ? (a += "File: " + e.xamlFile + "     \n", a += "Line: " + e.lineNumber + "     \n", a += "Position: " + e.charPosition + "     \n") : "RuntimeError" == n && (0 != e.lineNumber && (a += "Line: " + e.lineNumber + "     \n", a += "Position: " + e.charPosition + "     \n"), a += "MethodName: " + e.methodName + "     \n"), new Error(a) } }