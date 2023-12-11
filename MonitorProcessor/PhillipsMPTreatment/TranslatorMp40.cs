using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitorProcessor.PhillipsMPTreatment;

public class TranslatorMp40
{
    public static string TranslateParameter(string parameter)
    {
        switch (parameter)
        {
            case "NOM_ECG_CARD_BEAT_RATE":
                return "FC";

            case "NOM_PRESS_BLD_ART_ABP_SYS":
                return "PI_SIST";

            case "NOM_PRESS_BLD_ART_ABP_DIA":
                return "PI_DIAS";

            case "NOM_PRESS_BLD_ART_ABP_MEAN":
                return "PI_MED";

            case "NOM_PULS_OXIM_SAT_O2":
                return "SPO2";

            case "NOM_PLETH_PULS_RATE":
                return "SPO2_PR";

            case "NOM_TEMP_VEN":
                return "T1";

            case "NOM_RESP_RATE":
                return "FR";

            case "NOM_ECG_AMPL_ST_V5":
                return "ST_V5";

            case "NOM_ECG_AMPL_ST_V2":
                return "ST_V2";

            case "NOM_ECG_AMPL_ST_AVR":
                return "ST_AVR";

            case "NOM_ECG_AMPL_ST_AVL":
                return "ST_AVL";

            case "NOM_ECG_AMPL_ST_AVF":
                return "ST_AVF";

        }
        return parameter;
    }


}