// AUTOGENERATED FILE - DO NOT MODIFY!
// This file generated by Djinni from muse_file.djinni

#import <Foundation/Foundation.h>

/**
 * \if IOS_ONLY
 * \file
 * \endif
 * Represents all possible MuseData message data types in a .muse file.
 * This enum corresponds to the DataType enum in the protobuf schema.
 * For more information on the file format see: http://developer.choosemuse.com/file-formats/muse
 */
typedef NS_ENUM(NSInteger, IXNMessageType)
{
    /** A message containing eeg data. */
    IXNMessageTypeEeg,
    /** A message containing quantization data. */
    IXNMessageTypeQuantization,
    /** A message containing accelerometer data. */
    IXNMessageTypeAccelerometer,
    /** A message containing battery data. */
    IXNMessageTypeBattery,
    /** A message containing version data. */
    IXNMessageTypeVersion,
    /** A message containing configuration data. */
    IXNMessageTypeConfiguration,
    /** A message containing annotation data. */
    IXNMessageTypeAnnotation,
    /** A message containing histogram data. */
    IXNMessageTypeHistogram,
    /** A message containing algorithm data. */
    IXNMessageTypeAlgValue,
    /** A message containing dsp data. */
    IXNMessageTypeDsp,
    /** A message containing device data. */
    IXNMessageTypeComputingDevice,
    /** A message containing dropped eeg data. */
    IXNMessageTypeEegDropped,
    /** A message containing dropped acc data. */
    IXNMessageTypeAccDropped,
    /** A message containing data from the calm application. */
    IXNMessageTypeCalmApp,
    /** A message containing data from the calm algorithm. */
    IXNMessageTypeCalmAlg,
    /** A message containing muse element data. */
    IXNMessageTypeMuseElements,
    /** A message containing gyro data. */
    IXNMessageTypeGyro,
    /** A message containing artifact packet. */
    IXNMessageTypeArtifact,
};
