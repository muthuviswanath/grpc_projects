// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: Protos/test.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace GrpcStreamingServer {

  /// <summary>Holder for reflection information generated from Protos/test.proto</summary>
  public static partial class TestReflection {

    #region Descriptor
    /// <summary>File descriptor for Protos/test.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static TestReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "ChFQcm90b3MvdGVzdC5wcm90bxIEdGVzdCI0CgVUcmlhbBITCgtUZXN0TWVz",
            "c2FnZRgBIAEoCRIWCg53ZWxjb21lTWVzc2FnZRgCIAEoCTJBCgxUZXN0RGVt",
            "b1NlcnYSMQoTU2VydmVyU3RyZWFtaW5nRGVtbxILLnRlc3QuVHJpYWwaCy50",
            "ZXN0LlRyaWFsMAFCFqoCE0dycGNTdHJlYW1pbmdTZXJ2ZXJiBnByb3RvMw=="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::GrpcStreamingServer.Trial), global::GrpcStreamingServer.Trial.Parser, new[]{ "TestMessage", "WelcomeMessage" }, null, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class Trial : pb::IMessage<Trial>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<Trial> _parser = new pb::MessageParser<Trial>(() => new Trial());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<Trial> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::GrpcStreamingServer.TestReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Trial() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Trial(Trial other) : this() {
      testMessage_ = other.testMessage_;
      welcomeMessage_ = other.welcomeMessage_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Trial Clone() {
      return new Trial(this);
    }

    /// <summary>Field number for the "TestMessage" field.</summary>
    public const int TestMessageFieldNumber = 1;
    private string testMessage_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string TestMessage {
      get { return testMessage_; }
      set {
        testMessage_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "welcomeMessage" field.</summary>
    public const int WelcomeMessageFieldNumber = 2;
    private string welcomeMessage_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string WelcomeMessage {
      get { return welcomeMessage_; }
      set {
        welcomeMessage_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as Trial);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(Trial other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (TestMessage != other.TestMessage) return false;
      if (WelcomeMessage != other.WelcomeMessage) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (TestMessage.Length != 0) hash ^= TestMessage.GetHashCode();
      if (WelcomeMessage.Length != 0) hash ^= WelcomeMessage.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      output.WriteRawMessage(this);
    #else
      if (TestMessage.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(TestMessage);
      }
      if (WelcomeMessage.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(WelcomeMessage);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
      if (TestMessage.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(TestMessage);
      }
      if (WelcomeMessage.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(WelcomeMessage);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(ref output);
      }
    }
    #endif

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (TestMessage.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(TestMessage);
      }
      if (WelcomeMessage.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(WelcomeMessage);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(Trial other) {
      if (other == null) {
        return;
      }
      if (other.TestMessage.Length != 0) {
        TestMessage = other.TestMessage;
      }
      if (other.WelcomeMessage.Length != 0) {
        WelcomeMessage = other.WelcomeMessage;
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      input.ReadRawMessage(this);
    #else
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 10: {
            TestMessage = input.ReadString();
            break;
          }
          case 18: {
            WelcomeMessage = input.ReadString();
            break;
          }
        }
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
            break;
          case 10: {
            TestMessage = input.ReadString();
            break;
          }
          case 18: {
            WelcomeMessage = input.ReadString();
            break;
          }
        }
      }
    }
    #endif

  }

  #endregion

}

#endregion Designer generated code
