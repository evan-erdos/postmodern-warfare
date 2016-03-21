
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using YamlDotNet.Serialization;

public enum MessageTrigger { Default, Time, Event, Collider }

public enum Speaker {
    Default   = 0,
    MallowDog = 0xA12FFF,
    Commander = 0xFF0000,
    Chocolate = 0xFF00FF};


public struct Message {

	[YamlMember(Alias="trigger")]
	public MessageTrigger messageTrigger {get;set;}

    [YamlMember(Alias="speaker")]
    public Speaker speaker {get;set;}

    [YamlMember(Alias="persist")]
    public bool Persist {get;set;}

	[YamlMember(Alias="delay")]
	public float Delay {get;set;}

    [YamlMember(Alias="title")]
    public string Title {get;set;}

    [YamlMember(Alias="next")]
    public string Next {get;set;}

    [YamlMember(Alias="desc")]
    public string Description {get;set;}

    public Message(
                    string title,
                    string description,
                    float delay,
                    Speaker speaker,
                    MessageTrigger messageTrigger,
                    bool persist) : this() {
        this.Title = title;
        this.Description = description;
        this.Delay = delay;
        this.speaker = speaker;
        this.messageTrigger = messageTrigger;
        this.Persist = persist;
    }

    public Message(string title, string description)
        : this(title,description,0f,Speaker.Default,MessageTrigger.Default,false) { }


}
