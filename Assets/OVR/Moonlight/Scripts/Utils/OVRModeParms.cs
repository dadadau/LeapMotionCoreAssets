﻿/************************************************************************************

Copyright   :   Copyright 2014 Oculus VR, LLC. All Rights reserved.

Licensed under the Oculus VR Rift SDK License Version 3.2 (the "License");
you may not use the Oculus VR Rift SDK except in compliance with the License,
which is provided at the time of installation or download, or which
otherwise accompanies this software in either electronic or hard copy form.

You may obtain a copy of the License at

http://www.oculusvr.com/licenses/LICENSE-3.2

Unless required by applicable law or agreed to in writing, the Oculus VR SDK
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.

************************************************************************************/

using UnityEngine;
using VR = UnityEngine.VR;
using XR = UnityEngine.XR;

/// <summary>
/// Logs when the application enters power save mode and allows you to a low-power CPU/GPU level with a button press.
/// </summary>
public class OVRModeParms : MonoBehaviour
{
#region Member Variables

	/// <summary>
	/// The gamepad button that will switch the application to CPU level 0 and GPU level 1.
	/// </summary>
	public OVRGamepadController.Button	resetButton = OVRGamepadController.Button.X;	

#endregion

	/// <summary>
	/// Invoke power state mode test.
	/// </summary>
	void Start()
	{		
		if (!XR.XRDevice.isPresent)
		{
			enabled = false;
			return;
		}

		// Call TestPowerLevelState after 10 seconds 
		// and repeats every 10 seconds.
		InvokeRepeating ( "TestPowerStateMode", 10, 10.0f );
	}

	/// <summary>
	/// Change default vr mode parms dynamically.
	/// </summary>
	void Update()
	{
		// NOTE: some of the buttons defined in OVRGamepadController.Button are not available on the Android game pad controller
		if ( OVRGamepadController.GPC_GetButtonDown(resetButton)) 
		{
			//*************************
			// Dynamically change VrModeParms cpu and gpu level.
			// NOTE: Reset will cause 1 frame of flicker as it leaves
			// and re-enters Vr mode.
			//*************************
			OVRPlugin.cpuLevel = 0;
			OVRPlugin.gpuLevel = 1;
		}
	}

	/// <summary>
	/// Check current power state mode.
	/// </summary>
	void TestPowerStateMode()
	{
		//*************************
		// Check power-level state mode
		//*************************
		if (OVRPlugin.powerSaving)
		{
			// The device has been throttled
			Debug.Log("POWER SAVE MODE ACTIVATED");
		}
	}
}
