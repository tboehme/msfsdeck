namespace Loupedeck.MsfsPlugin.msfs
{
    using System;

    using static DataTransferTypes;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0049:Simplify Names", Justification = "<Pending>")]
    internal static class DataTransferIn
    {

        public static void ReadMsfsValues(Readers reader)
        {
            MsfsData.Instance.AircraftName = reader.title;

            SetMsfsValue(BindingKeys.ENGINE_AUTO, reader.E1On);
            SetMsfsValue(BindingKeys.AILERON_TRIM, (Int64)Math.Round(reader.aileronTrim * 100));
            SetMsfsValue(BindingKeys.AP_ALT, reader.apAltitude);
            SetMsfsValue(BindingKeys.ALT, reader.planeAltitude);
            SetMsfsValue(BindingKeys.KOHLSMAN, (Int64)Math.Round(reader.kohlsmanInHb * 10000));
            SetMsfsValue(BindingKeys.ELEVATOR_TRIM, (Int64)Math.Round(reader.elevatorTrim * 100));
            SetMsfsValue(BindingKeys.MAX_FLAP, reader.flapMax);
            SetMsfsValue(BindingKeys.FLAP, reader.flapPosition);
            SetMsfsValue(BindingKeys.AP_HEADING, reader.apHeading);
            SetMsfsValue(BindingKeys.HEADING, (Int64)Math.Round(reader.planeHeading));
            SetMsfsValue(BindingKeys.MIXTURE, reader.mixtureE1);
            SetMsfsValue(BindingKeys.PROPELLER, (Int64)Math.Round(reader.propellerE1));
            SetMsfsValue(BindingKeys.RUDDER_TRIM, (Int64)Math.Round(reader.rudderTrim * 100));
            SetMsfsValue(BindingKeys.AP_SPEED, reader.apSpeed);
            SetMsfsValue(BindingKeys.SPEED, reader.planeSpeed);
            SetMsfsValue(BindingKeys.SPOILER, (Int64)Math.Round(reader.spoiler * 100));
            SetMsfsValue(BindingKeys.THROTTLE, (Int64)Math.Round(reader.throttle * 100));
            SetMsfsValue(BindingKeys.AP_VSPEED, reader.apVSpeed);
            SetMsfsValue(BindingKeys.VSPEED, (Int64)Math.Round(reader.planeVSpeed * 60));
            SetMsfsValue(BindingKeys.PARKING_BRAKES, reader.parkingBrake);
            SetMsfsValue(BindingKeys.PITOT, reader.pitot);
            SetMsfsValue(BindingKeys.GEAR_RETRACTABLE, reader.gearRetractable);
            SetMsfsValue(BindingKeys.GEAR_FRONT, (Int64)Math.Round(reader.gearCenterPos * 10));
            SetMsfsValue(BindingKeys.GEAR_LEFT, (Int64)Math.Round(reader.gearLeftPos * 10));
            SetMsfsValue(BindingKeys.GEAR_RIGHT, (Int64)Math.Round(reader.gearRightPos * 10));
            SetMsfsValue(BindingKeys.FUEL_FLOW_GPH, (Int64)Math.Round(reader.FuelGph));
            SetMsfsValue(BindingKeys.FUEL_FLOW_PPH, (Int64)Math.Round(reader.FuelPph * 100.0));
            SetMsfsValue(BindingKeys.FUEL_PERCENT, (Int64)Math.Round(reader.fuelQuantity * 100.0 / reader.fuelCapacity));
            SetMsfsValue(BindingKeys.FUEL_TIME_LEFT, (Int64)(reader.fuelQuantity / reader.FuelGph * 3600));
            SetMsfsValue(BindingKeys.AP_NEXT_WP_ID, reader.wpID);
            SetMsfsValue(BindingKeys.AP_NEXT_WP_DIST, (Int64)Math.Round(reader.wpDistance * 0.00053996f * 10, 1));
            SetMsfsValue(BindingKeys.AP_NEXT_WP_ETE, reader.wpETE);
            SetMsfsValue(BindingKeys.AP_NEXT_WP_HEADING, reader.wpBearing);
            SetMsfsValue(BindingKeys.ENGINE_TYPE, reader.engineType);
            SetMsfsValue(BindingKeys.ENGINE_NUMBER, reader.engineNumber);
            SetMsfsValue(BindingKeys.E1RPM, reader.ENG1N1RPM);
            SetMsfsValue(BindingKeys.E2RPM, reader.ENG2N1RPM);
            SetMsfsValue(BindingKeys.E3RPM, reader.ENG3N1RPM);
            SetMsfsValue(BindingKeys.E4RPM, reader.ENG4N1RPM);
            SetMsfsValue(BindingKeys.E1N1, PercentValue(reader.E1N1));
            SetMsfsValue(BindingKeys.E2N1, PercentValue(reader.E2N1));
            SetMsfsValue(BindingKeys.E3N1, PercentValue(reader.E3N1));
            SetMsfsValue(BindingKeys.E4N1, PercentValue(reader.E4N1));
            SetMsfsValue(BindingKeys.E1N2, PercentValue(reader.E1N2));
            SetMsfsValue(BindingKeys.E2N2, PercentValue(reader.E2N2));
            SetMsfsValue(BindingKeys.E3N2, PercentValue(reader.E3N2));
            SetMsfsValue(BindingKeys.E4N2, PercentValue(reader.E4N2));
            SetMsfsValue(BindingKeys.PUSHBACK_ATTACHED, (reader.pushbackAttached == 1 && reader.wheelRPM != 0) ? 1 : 0);
            SetMsfsValue(BindingKeys.PUSHBACK_STATE, reader.onGround);
            //SetMsfsValue(BindingKeys.PUSHBACK_ANGLE, reader.pushback); // Can read but set so stay on the controller state

            SetMsfsValue(BindingKeys.LIGHT_NAV, reader.navLight);
            SetMsfsValue(BindingKeys.LIGHT_BEACON, reader.beaconLight);
            SetMsfsValue(BindingKeys.LIGHT_LANDING, reader.landingLight);
            SetMsfsValue(BindingKeys.LIGHT_TAXI, reader.taxiLight);
            SetMsfsValue(BindingKeys.LIGHT_STROBE, reader.strobeLight);
            SetMsfsValue(BindingKeys.LIGHT_INSTRUMENT, reader.panelLight);
            SetMsfsValue(BindingKeys.LIGHT_RECOG, reader.recognitionLight);
            SetMsfsValue(BindingKeys.LIGHT_WING, reader.wingLight);
            SetMsfsValue(BindingKeys.LIGHT_LOGO, reader.logoLight);
            SetMsfsValue(BindingKeys.LIGHT_CABIN, reader.cabinLight);
            SetMsfsValue(BindingKeys.LIGHT_PEDESTAL, reader.pedestralLight);
            SetMsfsValue(BindingKeys.LIGHT_GLARESHIELD, reader.glareshieldLight);

            SetMsfsValue(BindingKeys.AP_ALT_SWITCH, reader.apAltHold);
            SetMsfsValue(BindingKeys.AP_HEAD_SWITCH, reader.apHeadingHold);
            SetMsfsValue(BindingKeys.AP_NAV_SWITCH, reader.apNavHold);
            SetMsfsValue(BindingKeys.AP_SPEED_SWITCH, reader.apSpeedHold);
            SetMsfsValue(BindingKeys.AP_MASTER_SWITCH, reader.apMasterHold);
            SetMsfsValue(BindingKeys.AP_THROTTLE_SWITCH, reader.apThrottleHold);
            SetMsfsValue(BindingKeys.AP_VSPEED_SWITCH, reader.apVerticalSpeedHold);
            SetMsfsValue(BindingKeys.AP_YAW_DAMPER_SWITCH, reader.apYawDamper);
            SetMsfsValue(BindingKeys.AP_BC_SWITCH, reader.apBackCourse);

            SetMsfsValue(BindingKeys.AP_FD_SWITCH, reader.apFD);
            SetMsfsValue(BindingKeys.AP_FLC_SWITCH, reader.apFLC);
            SetMsfsValue(BindingKeys.AP_APP_SWITCH, reader.apAPP);
            SetMsfsValue(BindingKeys.AP_LOC_SWITCH, reader.apLOC);

            SetMsfsValue(BindingKeys.COM1_ACTIVE_FREQUENCY, reader.COM1ActiveFreq);
            SetMsfsValue(BindingKeys.COM1_STBY, reader.COM1StbFreq);
            SetMsfsValue(BindingKeys.COM1_AVAILABLE, reader.COM1Available);
            //SetMsfsValue(BindingKeys.COM1_STATUS, reader.COM1Status);
            //SetMsfsValue(BindingKeys.COM1_ACTIVE_FREQUENCY_TYPE, COMtypeToInt(reader.COM1Type));

            SetMsfsValue(BindingKeys.COM2_ACTIVE_FREQUENCY, reader.COM2ActiveFreq);
            SetMsfsValue(BindingKeys.COM2_STBY, reader.COM2StbFreq);
            SetMsfsValue(BindingKeys.COM2_AVAILABLE, reader.COM2Available);
            //SetMsfsValue(BindingKeys.COM2_STATUS, reader.COM2Status);
            //SetMsfsValue(BindingKeys.COM2_ACTIVE_FREQUENCY_TYPE, this.COMtypeToInt(reader.COM2Type));

            SetMsfsValue(BindingKeys.SIM_RATE, (Int64)(reader.simRate * 100));
            SetMsfsValue(BindingKeys.SPOILERS_ARM, reader.spoilerArm);

            SetMsfsValue(BindingKeys.NAV1_ACTIVE_FREQUENCY, reader.NAV1ActiveFreq);
            SetMsfsValue(BindingKeys.NAV1_AVAILABLE, reader.NAV1Available);
            SetMsfsValue(BindingKeys.NAV1_STBY_FREQUENCY, reader.NAV1StbyFreq);
            SetMsfsValue(BindingKeys.NAV2_ACTIVE_FREQUENCY, reader.NAV2ActiveFreq);
            SetMsfsValue(BindingKeys.NAV2_AVAILABLE, reader.NAV2Available);
            SetMsfsValue(BindingKeys.NAV2_STBY_FREQUENCY, reader.NAV2StbyFreq);

            SetMsfsValue(BindingKeys.VOR1_OBS, reader.NAV1Obs);
            SetMsfsValue(BindingKeys.VOR2_OBS, reader.NAV2Obs);

            SetMsfsValue(BindingKeys.AIR_TEMP, (long)Math.Round(reader.AirTemperature * 10));
            SetMsfsValue(BindingKeys.WIND_DIRECTION, reader.WindDirection);
            SetMsfsValue(BindingKeys.WIND_SPEED, reader.WindSpeed);
            SetMsfsValue(BindingKeys.VISIBILITY, reader.Visibility);
            SetMsfsValue(BindingKeys.SEA_LEVEL_PRESSURE, (long)Math.Round(reader.SeaLevelPressure * 10));

            SetMsfsValue(BindingKeys.ADF_ACTIVE_FREQUENCY, (long)Math.Round(reader.ADFActiveFreq * 10));
            SetMsfsValue(BindingKeys.ADF_STBY_FREQUENCY, (long)Math.Round(reader.ADFStbyFreq * 10));
            SetMsfsValue(BindingKeys.ADF1_AVAILABLE, reader.ADF1Available);
            SetMsfsValue(BindingKeys.ADF1_STBY_AVAILABLE, reader.ADF2Available);

            //++ Insert appropriate SetMsfsValue calls here using the new binding keys and the new fields in reader.

            MsfsData.Instance.Changed();
        }

        // Percentages are rounded to nearest integer value:
        static Int64 PercentValue(Double value) => (Int64)Math.Round(value);

        static void SetMsfsValue(BindingKeys key, long value) => MsfsData.Instance.bindings[key].SetMsfsValue(value);

       
        
    }
}
