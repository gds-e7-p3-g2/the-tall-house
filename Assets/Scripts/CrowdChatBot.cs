using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace IStreamYouScream
{
    class Messages : List<string> { };
    class EventToMessages : Dictionary<UnityEvent, Messages> { };
    class EventToCooldown : Dictionary<UnityEvent, bool> { };

    public class CrowdChatBot : MonoBehaviour
    {
        private EventToMessages event2msg;
        private EventToCooldown event2cool = new EventToCooldown();
        [SerializeField] private float timeToCooldown = 5f;
        [SerializeField]
        private List<string> Usernames = new List<string>() {
            "m3liss4", "jm", "case_diya", "CH1M3R4", "L16H7N1N6", "Silversgleaming", "emOCoDe", "pacificfish", "pleaseamazeme", "icraveattention", "pinotnoir", "redrum20", "warrensfan1997", "sonicf2302", "mannieq", "lovehuskie", "lustforviews", "imjustbored", "bl33der", "late_boomer", "catatafish", "needkicks", "nothingmatters230913", "Patryk_N", "xAxAERS_o_O", "rEaDy10", "khanlord_2020", "idontsleep", "hikimori1986", "rosebud", "kimik0", "wenflon", "sridevi4ever", "saj28", "slither", "ashiscallingme", "kraft_punk", "ramadanSteve", "patricia_the_stripper", "cobra_kai20", "akamarU", "souchi", "tomie", "uzumaki_96", "stonerBoy2006", "edgy_teddy", "Peter_Paka", "PrinceAlbert", "itsamemario", "cococubana", "ladyjessica", "holdthedoor", "bruce_willis_was_a_ghost", "patatapon_nyc", "acdcfan13941", "m_myers", "hedonisticApe", "mothra_foka",
        };

        private List<Color> Colors = new List<Color>() {
            Color.red, Color.blue, Color.cyan, Color.gray, Color.yellow
        };

        private Dictionary<string, Color> name2color = new Dictionary<string, Color>();

        void Start()
        {
            event2msg = new EventToMessages() {
                {
                    StoryEvents.Instance.OnFamilyPictureRecorded,
                    new Messages() {
                        "creepy!",
                        "the father look kinda familiar idk",
						"like father like son",
						"seriously father looks familiar!!!!",
						"that guy... why i feel like I saw that face before???",
						"that old dude was a kid on a different painting i think?",
						"that poor family! what happened to them",
						"you think the ghost killed them?",
						"are they the ghosts here?",
						"one weird family",
						"so stiff",
                    }
                },
                { StoryEvents.Instance.OnGrandfatherPictureRecorder, new Messages() {
						"Who is that",
						"that kid looks sad :(",
						"yo is that harry p :D",
						"wheres mom? :(",
						"they look rich",
						"pretty sure this old guy is a freaking ghost here",
                }},
                {
                    StoryEvents.Instance.OnGhostRecorded,
                    new Messages() {
                        "holy shit a real ghost :O",
						"gotta show this to my brah",
						"HOLY MACCARONI",
						"wild",
						"lol thats not a real ghost",
                        "this is going viral",
						"clearly FAKE",
						"OMFG WHAT THE HELL IS THAT A GHOST",
						"LOL wtf thats real?!",
						"that makes me question what happens after death",
						"please show us more of it!",
						"W H O A",
						"how comes camera makes it weaker? Is it magical or sth?",
						"hello nightmares",
						"ok so ghost exist",
						"aw hell no! Kill this damn thing",
						"record it to death YEAH :D",
						"record the shit out of it",
						"keep recording man it bums it out :o",
						"ghost is annoyed",
						"how cool is that",
						"dude are you like a ghostbuster",
						"harv you are killin it bro",
						"this cant be real yo",
						"I am really shit*ing my pants rn",
						"guys, its clearly fake I mean???",
						"expensive props",
						"a spirit caught on camera HOLY SHIT",
						"its official: life after death is real",
						"omg omg omg omg omg WTF IS THAT",
						"LOL NO WAY!!!!",
						":O",
                        "cgi",
                        "OMG",
                        "im not sleeping tonight",
                        "so wait ghosts are real",
                        "cleverandsceptical: whatever its all fake",
                        "holy f**k! an actual ghost",
                        "show us more",
                        "its getting weaker?",
                        "the camera weakens it? somehow?",
						"this is not real WHAT THE HECK",
                        "power of modern technology :D",
                        "thats one tense ghost",
                    }
                },
                { StoryEvents.Instance.OnGhostStunned, new Messages() { 
						"yo he just got puffed",
						"haha no way!",
						"slay that ghost",
						"you wont hurt it with that but at least buys u some time",
						"obvi You cant kill a ghost",
						"ghost cant be killed duh",
						"he just gonna respawn soon so RUN BIATCH",
						"no freaking way he just stunned the ghost",
						"RUN",
						"dude better run or hide or something",
						"better stay out of his way now",
						"swing swing swing swing",
						"dafuq ghost just got whacked",
				}},
                { StoryEvents.Instance.OnPlayerStunned, new Messages() { 
						"what a joke",
						"you cant even beat a ghost! :D",
						"ghost is not even attacking... this is embarassing",
						"dude ghost so scary that doesnt even need to attack lol",
						"wrecked by a ghost...weak!",
						"aw hope Im not watching some lame ass found footage bs!",
						"WEAK AF",
						"he afk?",
						"LOLOLOLOLOLOLOLOLOLOLOLOLOLOL",
						"embarassing",
						"cringe!",
						"its too sad to watch",
						"why am I even watching this?",
						"look at this pathetic guy getting whacked by a ghost lol",
						"lol WUT",
						"cringefest!",
						"SO. SAD.",
						"ZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZ",
						"is he doing this for purpose???????????",
						"and I thought Im sad person",
						"oh shiet he is freaking out",
						"lul this goes on youtube best freakouts",
						"this is pure cringe",
						"fight bro fight",
						"im not sure if I should stay",
						"PATHETIC",
						"hahahahahahahahaha",
						"he weak",
						"he needs some milk!",
						"chicken little :D",
						"oh come on!",
						"we need ACTION dude",
						"record some freakin ghosts",
						"do something brooooo",
						}
				},
                {
                    StoryEvents.Instance.OnMeleeUsed,
                    new Messages() {
                        "Don't swing the phone",
						"yeah so technology to the rescue i guess",
						"yes that will show him",
						"whats going on",
						"wow what a weak swing XD",
                        "Whoa dizzy",
						"He uses phone as a melee",
						"Wee",
						"dafuq shaky cam",
						"dont overuse it harvey",
						"WE CANT SEE ANYTHING WHEN YOU DO THIS",
						"dat swing tho",
						"easy with the phone bro",
                    }
                },
                { StoryEvents.Instance.OnMusicboxFirstSpotted, new Messages() { 
						"love those!",
						"awwww",
						"oldschool!",
						"an old jukebox wow, soooo impresive",
						"for some reason I think its important? i dunno looks too cool for the place",
						"lul play sum music :D",
						"dude is that ghost somehow connected to this?",
						}},
                { StoryEvents.Instance.OnCoinInsertedToMusicbox, new Messages() { 
						"WHACK?!",
						"wow thats creepy af",
						"bloody letters on the wall, how creepy....",
						"so I guess family was WHACKing people?",
						"I know this name! :O",
						"WHACK was my fav band in the 90... they dissapeared years ago?",
						"wait wasnt WHACK the band that vanished during the tour?",
				}},
                { StoryEvents.Instance.OnMusicboxWrongChoice, new Messages() { "OnMusicboxWrongChoice GENERIC MESSAGE"}},
                { StoryEvents.Instance.OnMusicboxWrongChoiceAgain, new Messages() { "OnMusicboxWrongChoiceAgain GENERIC MESSAGE"}},
                { StoryEvents.Instance.OnGroundFloorPicturesRecorded, new Messages() { "OnGroundFloorPicturesRecorded GENERIC MESSAGE"}},
                { StoryEvents.Instance.OnMusicanBodyFound, new Messages() { 
						"GROSS!!!!! :O",
						"GORE!!!!! :D",
						"now thats the content I was waiting for!",
						"ok i MIGHT know who that is",
						"dude he looks like a rocker or sth",
						}},
                { StoryEvents.Instance.OnBoringPeriod, new Messages() { 
						"hey why nothings happening",
						"thats it. he's running out of steam",
						"sadly, nothing is happening",
						"dude at least try to investigate",
						"plz do something interesting",
						"why u no record good stuff",
						"so much about riding that wave",
						"all you had to do was filming goddamn ghost cj!",
						"gosh I thought its a simple task to keeep people interested in HAUNTED house",
						"boooooooooring",
						"will this guy do something interesting or should i leave?",
						}},
                { StoryEvents.Instance.OnRandom, new Messages() { 
						"please watch my channel, reviewing the best vegetables on a market"}},
                { StoryEvents.Instance.OnTick, new Messages() { "OnTick GENERIC MESSAGE"}}
            };
            BindEvents();
        }

        private void BindEvents()
        {
            foreach (KeyValuePair<UnityEvent, Messages> entry in event2msg)
            {
                entry.Key.AddListener(() => SendRandomMessage(entry.Key, entry.Value));
            }
        }

        private string GetRandomUsername()
        {
            return Usernames[Random.Range(0, Usernames.Count)];
        }

        private Color GetUserColor(string username)
        {
            if (!name2color.ContainsKey(username))
            {
                name2color[username] = Colors[Random.Range(0, Colors.Count)];
            }
            return name2color[username];
        }

        private void SendRandomMessage(UnityEvent e, Messages m)
        {
            if (event2cool.ContainsKey(e) && event2cool[e])
            {
                return;
            }

            string username = GetRandomUsername();
            Color color = GetUserColor(username);
            string colorHex = ColorUtility.ToHtmlStringRGB(color);
            string message = m[Random.Range(0, m.Count)];

            ChatMessagesContainer.Instance.AddMessage($"<color=#{colorHex}>{username}:</color>: {message}");

            StartCoroutine(Cooldown(e));
        }

        private IEnumerator Cooldown(UnityEvent e)
        {
            event2cool[e] = true;

            yield return new WaitForSeconds(timeToCooldown);

            event2cool[e] = false;
        }
    }
}