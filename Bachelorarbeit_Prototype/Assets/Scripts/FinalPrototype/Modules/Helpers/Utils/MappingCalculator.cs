using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** @class MappingCalculator This class offers a mapping function
 */
public static class MappingCalculator 
{
    /**
     * Maps a value from one range to another
     * @param val input value
     * @param in1 lower end of input range
     * @param in2 upper end of input range
     * @param out1 lower end of output range
     * @param out2 upper end of output range
     * @return calculated value
     */
    public static float Remap(float val, float in1, float in2, float out1, float out2)
    {
        return out1 + (val - in1) * (out2 - out1) / (in2 - in1);
    }
}
