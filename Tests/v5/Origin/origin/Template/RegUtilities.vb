Option Strict On 'Of course

Imports Microsoft.Win32

Namespace Common

    ''' <summary>
    ''' Performs Registry operations of various kinds
    ''' 
    ''' This file is shared between projects, alter it carefully!
    ''' </summary>
    Friend Class RegUtilities

#Region " Constructor "
		Private Sub New()

		End Sub
#End Region

#Region " Get "

        ''' <summary>
        ''' Get the value of the specified key, item
        ''' </summary>
        ''' GetKeyValue
        ''' 
        ''' L�ser fr�n registret
        ''' GetKeyValue(Microsoft.Win32.RegistryHive.LocalMachine, "SOFTWARE\xxx", "DB", "")
        ''' eller
        ''' GetKeyValue("HKEY_LOCAL_MACHINE\SOFTWARE\xxx", "DB", "")
        ''' <param name="hive">Which root key to use</param>
        ''' <param name="subKey">The key to use as a base</param>
        ''' <param name="item">Which item to try to get the value from</param>
        ''' <param name="defaultValue">The value to use, if no value/item was found</param>
        ''' <returns>Get the value of the specified key, item</returns>
        Friend Shared Function GetKeyValue(hive As RegistryHive, subKey As String, item As String, defaultValue As String) As String
            Try
                Dim value As String
                Using vBaseKey = GetBaseKey(hive)
                    Using vRegKey = vBaseKey.OpenSubKey(subKey, False)
                        value = CType(vRegKey.GetValue(item, defaultValue), String)
                    End Using
                End Using

                Return value
            Catch ex As Exception
                Return defaultValue
            End Try
		End Function

        ''' <summary>
        ''' Get the value of the specified key, item
        ''' </summary>
        ''' <param name="key">The entire key to use as a base</param>
        ''' <param name="item">Which item to try to get the value from</param>
        ''' <param name="defaultValue">The value to use, if no value/item was found</param>
        ''' <returns>The value of the specified key, item</returns>
        Friend Shared Function GetKeyValue(key As String, item As String, defaultValue As String) As String
            Try
                Dim value As String

                Using baseKey = GetBaseKey(GetHive(key))
                    If baseKey Is Nothing Then
                        Return defaultValue
                    End If

                    Using regKey = baseKey.OpenSubKey(GetSubKey(key), False)
                        If regKey Is Nothing Then
                            Return defaultValue
                        End If

                        value = CType(regKey.GetValue(item, defaultValue), String)
                    End Using
                End Using

                Return value
            Catch
                Return defaultValue
            End Try
		End Function
#End Region

#Region " Set "

        ''' <summary>
        ''' Write a specified value to the selected key, item
        ''' </summary>
        ''' SetKeyValue
        ''' 
        ''' Skriver till registret
        ''' SetKeyValue(Microsoft.Win32.RegistryHive.LocalMachine, "SOFTWARE\xxx", "DB", "Nytt")
        ''' eller
        ''' SetKeyValue("HKEY_LOCAL_MACHINE\SOFTWARE\xxx", "DB", "Nytt")
        ''' <param name="hive">Which root key to use</param>
        ''' <param name="subKey">The key to use as a base</param>
        ''' <param name="item">The item to get its value set</param>
        ''' <param name="newValue">The new value of the item</param>
        Friend Shared Function SetKeyValue(hive As RegistryHive, subKey As String, item As String, newValue As String) As Boolean
            Try
                Using baseKey = GetBaseKey(hive)
                    Using regKey = baseKey.OpenSubKey(subKey, True)
                        If regKey IsNot Nothing Then
                            regKey.SetValue(item, newValue)
                        Else
                            'Ooops d� fanns den inte, skapa
                            If CreateNewKey(hive, subKey) Then
                                Using regKeyNew = baseKey.OpenSubKey(subKey, True)
                                    regKeyNew.SetValue(item, newValue)
                                End Using
                            Else
								Return False
							End If
						End If
					End Using
				End Using

				Return True
			Catch
				Return False
			End Try
		End Function

        ''' <summary>
        ''' Write a specified value to the selected key, item
        ''' </summary>
        ''' <param name="key">The entire key to use as a base</param>
        ''' <param name="item">The item to get its value set</param>
        ''' <param name="newValue">The new value of the item</param>
        Friend Shared Function SetKeyValue(key As String, item As String, newValue As String) As Boolean
            Try
                Using baseKey = GetBaseKey(GetHive(key))
                    Using regKey = baseKey.OpenSubKey(GetSubKey(key), True)
                        If regKey IsNot Nothing Then
                            regKey.SetValue(item, newValue)
                        Else
                            'Ooops d� fanns den inte, skapa
                            If CreateNewKey(key) Then
                                Using regKeyNew = baseKey.OpenSubKey(GetSubKey(key), True)
                                    regKeyNew.SetValue(item, newValue)
                                End Using
                            Else
                                Return False
                            End If
                        End If
                    End Using
                End Using

				Return True
			Catch
				Return False
			End Try
		End Function

        ''' <summary>
        ''' Write a specified value to the selected key, item
        ''' </summary>
        '''Dim bytUserGlobalPowerPolicy As Byte() = {1, 0, 0, 0, 6, 0, 0, 0, 3, 0, 0, 0, 0, 0, 0, 0, 6, 0, 0, 0, 3, 0, 0, 0, 0, 0, 0, 0, 2, 0, 0, 0, 3, 0, 0, 0, 0, 0, 0, 0, 2, 0, 0, 0, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 3, 0, 0, 0, 2, 0, 0, 0, 4, 0, 0, 192, 1, 0, 0, 0, 4, 0, 0, 0, 1, 0, 0, 0, 10, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 1, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0, 22, 0, 0, 0}
        '''[HKEY_CURRENT_USER\Control Panel\PowerCfg\GlobalPowerPolicy]
        '''"Policies"=hex:01,00,00,00,06,00,00,00,03,00,00,00,00,00,00,00,06,00,00,00,03,\
        '''  00,00,00,00,00,00,00,02,00,00,00,03,00,00,00,00,00,00,00,02,00,00,00,03,00,\
        '''  00,00,00,00,00,00,00,00,00,00,01,00,00,00,00,00,00,00,00,00,00,00,01,00,00,\
        '''  00,00,00,00,00,01,00,00,00,03,00,00,00,02,00,00,00,04,00,00,c0,01,00,00,00,\
        '''  04,00,00,00,01,00,00,00,0a,00,00,00,00,00,00,00,03,00,00,00,01,00,01,00,01,\
        '''  00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,02,00,00,00,\
        '''  00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,03,00,00,00,00,\
        '''  00,16,00,00,00
        ''' <param name="hive">Which root key to use</param>
        ''' <param name="subKey">The key to use as a base</param>
        ''' <param name="item">The item to get its value set</param>
        ''' <param name="newValue">The new value of the item</param>
        Friend Shared Function SetKeyValue(hive As RegistryHive, subKey As String, item As String, newValue As Byte()) As Boolean
            Try
                Using baseKey = GetBaseKey(hive)
                    Using regKey = baseKey.OpenSubKey(subKey, True)
                        If regKey IsNot Nothing Then
                            regKey.SetValue(item, newValue)
                        Else
                            'Ooops d� fanns den inte, skapa
                            If CreateNewKey(hive, subKey) Then
                                Using regKeyNew = baseKey.OpenSubKey(subKey, True)
                                    regKeyNew.SetValue(item, newValue)
                                End Using
                            Else
								Return False
							End If
						End If
					End Using
				End Using

				Return True
			Catch
				Return False
			End Try
		End Function

#End Region

#Region " Delete "

        ''' <summary>
        ''' Delete the key and all subkeys
        ''' </summary>
        ''' DeleteKey
        ''' 
        ''' Tar bort fr�n registret
        ''' DeleteKeyValue(Microsoft.Win32.RegistryHive.LocalMachine, "SOFTWARE\xxx")
        ''' eller
        ''' DeleteKeyValue("HKEY_LOCAL_MACHINE\SOFTWARE\xxx")
        ''' <param name="hive">Which root node to use as a base</param>
        ''' <param name="subKey">The Key to delete, including all subkeys</param>
        ''' <returns>True if the key was deleted</returns>
        Friend Shared Function DeleteKey(hive As RegistryHive, subKey As String) As Boolean
            Try
                Using baseKey = GetBaseKey(hive)
                    baseKey.DeleteSubKeyTree(subKey)
                End Using

				Return True
			Catch
				Return False
			End Try
		End Function

        ''' <summary>
        ''' Delete the key and all subkeys
        ''' </summary>
        ''' DeleteKey
        ''' 
        ''' Tar bort fr�n registret
        ''' DeleteKey(Microsoft.Win32.RegistryHive.LocalMachine, "SOFTWARE\xxx")
        ''' eller
        ''' DeleteKey("HKEY_LOCAL_MACHINE\SOFTWARE\xxx")
        ''' <param name="key">The entire path</param>
        ''' <returns>True if the key was deleted</returns>
        Friend Shared Function DeleteKey(key As String) As Boolean
            Try
                Using baseKey = GetBaseKey(GetHive(key))
                    baseKey.DeleteSubKeyTree(GetSubKey(key))
                End Using

				Return True
			Catch
				Return False
			End Try
		End Function

        ''' <summary>
        ''' Delete the value of a specified key
        ''' </summary>
        ''' DeleteKeyValue
        ''' 
        ''' Tar bort fr�n registret
        ''' DeleteKeyValue(Microsoft.Win32.RegistryHive.LocalMachine, "SOFTWARE\xxx", "DB")
        ''' eller
        ''' DeleteKeyValue("HKEY_LOCAL_MACHINE\SOFTWARE\xxx", "DB")
        ''' <param name="hive">Which root node to use as a base</param>
        ''' <param name="subKey">The Key in which the Item is found</param>
        ''' <param name="item">The Item to get its value deleted</param>
        ''' <returns>True if the value was deleted</returns>
        Friend Shared Function DeleteKeyValue(hive As RegistryHive, subKey As String, item As String) As Boolean
            Try
                Using baseKey = GetBaseKey(hive)
                    Using regKey = baseKey.OpenSubKey(subKey, True)
                        regKey.DeleteValue(item)
                    End Using
                End Using

				Return True
			Catch
				Return False
			End Try
		End Function

        ''' <summary>
        ''' Delete the value of a specified key
        ''' </summary>
        ''' <param name="key">The entire path</param>
        ''' <param name="item">The Item to get its value deleted</param>
        ''' <returns>Delete the value of a specified key</returns>
        Friend Shared Function DeleteKeyValue(key As String, item As String) As Boolean
            Try
                Using baseKey = GetBaseKey(GetHive(key))
                    Using regKey = baseKey.OpenSubKey(GetSubKey(key), True)
                        regKey.DeleteValue(item)
                    End Using
                End Using

				Return True
			Catch
				Return False
			End Try
		End Function

#End Region

#Region " Create "

        ''' <summary>
        ''' Creates a new registry key if it doesn't exist
        ''' </summary>
        ''' CreateNewKey
        ''' 
        ''' Skriver till registret
        ''' CreateSubKey(Microsoft.Win32.RegistryHive.LocalMachine, "SOFTWARE\xxx")
        ''' eller
        ''' CreateSubKey("HKEY_LOCAL_MACHINE\SOFTWARE\xxx")
        ''' <param name="hive">Which root node to use as a base</param>
        ''' <param name="subKey">The new key</param>
        ''' <returns>True if the key was created</returns>
        Friend Shared Function CreateNewKey(hive As RegistryHive, subKey As String) As Boolean
            Try
                Using baseKey = GetBaseKey(hive)
                    Using regKey = baseKey.CreateSubKey(subKey)
                    End Using
                End Using

				Return True
			Catch
				Return False
			End Try
		End Function

        ''' <summary>
        ''' Creates a new registry key if it doesn't exist
        ''' </summary>
        ''' <param name="key">The entire path to be created</param>
        ''' <returns>True if the key was created</returns>
        Friend Shared Function CreateNewKey(key As String) As Boolean
            Try
                Using baseKey = GetBaseKey(GetHive(key))
                    Using regKey = baseKey.CreateSubKey(GetSubKey(key))
                    End Using
                End Using
				Return True
			Catch
				Return False
			End Try
		End Function

#End Region

#Region " Enum "

        ''' <summary>
        ''' List all subkeys thet exist under a specified key
        ''' </summary>
        ''' Returnerar en lista av namn p� nycklar som ligger direkt under p_strSubKey. OBS, ENDAST nyckelnamn, inte v�rden.
        ''' <param name="hive">Which root key to use</param>
        ''' <param name="subKey">Which key to use as a base</param>
        ''' <returns>A list of key names</returns>
        Friend Shared Function EnumKeys(hive As RegistryHive, subKey As String) As String()
            Try
                Dim result() As String

                Using baseKey = GetBaseKey(hive)
                    Using regKey = baseKey.OpenSubKey(subKey, False)
                        result = regKey.GetSubKeyNames
                    End Using
                End Using

                Return result
            Catch
				Return Nothing
			End Try

		End Function

        ''' <summary>
        ''' List all values thet exist under a specified key
        ''' </summary>
        ''' Returnerar en lista av namn p� v�rden som ligger direkt i p_strSubKey.
        ''' <param name="hive">Which root key to use</param>
        ''' <param name="subKey">Which key to use as a base</param>
        ''' <returns>A list of values</returns>
        Friend Shared Function EnumValues(hive As RegistryHive, subKey As String) As String()
            Try
                Dim result() As String

                Using baseKey = GetBaseKey(hive)
                    Using regKey = baseKey.OpenSubKey(subKey, False)
                        result = regKey.GetValueNames
                    End Using
                End Using

                Return result
            Catch
				Return Nothing
			End Try

		End Function

#End Region

#Region " Private tools "

        ''' <summary>
        ''' Get the Hive that corresponds with a specified key
        ''' </summary>
        ''' GetHive
        ''' <param name="key">Enter the key</param>
        Private Shared Function GetHive(key As String) As String
            Dim vPos As Integer = InStr(key, "\")

            If vPos > 0 Then
                Return Left(key, vPos - 1)
            Else
				Return String.Empty
			End If
		End Function

        ''' <summary>
        ''' Get the last key in the specified complete key
        ''' </summary>
        ''' GetSubKey
        ''' <param name="key">The entire key</param>
        ''' <returns>The rightmost part of the string, after the last backslash</returns>
        Private Shared Function GetSubKey(key As String) As String
            Dim pos As Integer = InStr(key, "\")

            If pos > 0 Then
                Return Right(key, Len(key) - pos)
            Else
				Return String.Empty
			End If
		End Function

        ''' <summary>
        ''' Get the RegistryKey that corresponds with a specified hive
        ''' </summary>
        ''' GetBaseKey
        ''' 
        ''' Returnerar en pekare till en RegistryKey
        ''' <param name="hive">Select the hive</param>
        ''' <returns>Select the hive</returns>
        Private Shared Function GetBaseKey(hive As String) As RegistryKey
            Dim baseKey As RegistryKey

            Select Case hive
                Case "HKEY_CLASSES_ROOT"
                    baseKey = Registry.ClassesRoot
                Case "HKEY_CURRENT_CONFIG"
                    baseKey = Registry.CurrentConfig
                Case "HKEY_CURRENT_USER"
                    baseKey = Registry.CurrentUser
                Case "HKEY_LOCAL_MACHINE"
                    baseKey = Registry.LocalMachine
                Case "HKEY_USERS"
                    baseKey = Registry.Users
                Case Else
                    baseKey = Registry.CurrentUser 'Just for fail safe
            End Select

            Return baseKey
        End Function

        ''' <summary>
        ''' Get the RegistryKey that corresponds with a specified hive
        ''' </summary>
        ''' <param name="hive">Select the hive</param>
        ''' <returns>The RegistryKey</returns>
        Private Shared Function GetBaseKey(hive As RegistryHive) As RegistryKey
            Dim baseKey As RegistryKey

            Select Case hive
                Case RegistryHive.ClassesRoot
                    baseKey = Registry.ClassesRoot
                Case RegistryHive.CurrentConfig
                    baseKey = Registry.CurrentConfig
                Case RegistryHive.CurrentUser
                    baseKey = Registry.CurrentUser
                Case RegistryHive.LocalMachine
                    baseKey = Registry.LocalMachine
                Case RegistryHive.PerformanceData
                    baseKey = Registry.PerformanceData
                Case RegistryHive.Users
                    baseKey = Registry.Users
                Case Else
                    baseKey = Registry.CurrentUser
            End Select

            Return baseKey
        End Function

#End Region

	End Class
End Namespace