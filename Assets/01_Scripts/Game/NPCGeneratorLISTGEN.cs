using UnityEngine;
using TMPro;
using System.IO;

public class NPCGeneratorLISTGEN : MonoBehaviour
{
    public TMP_InputField nameField;
    public TMP_InputField descriptionField;
    public TMP_InputField plotHookField;
    public TMP_InputField statsField;
    public TMP_InputField occupationField;
    public TMP_InputField appearanceField;
    public TMP_InputField personalityField;
    public TMP_InputField inventoryField;
    public TMP_InputField quoteField;
    public TMP_InputField backStoryField;

    private string[] statNames = {
        "Strength", "Dexterity", "Constitution", "Intelligence", "Wisdom", "Charisma"
    };

    private string[] names = {
    "Arin", "Belra", "Caelum", "Doran", "Elira", "Fenric", "Gavric", "Hessia", "Ithran", "Jorven",
    "Kaelis", "Liora", "Marek", "Nyra", "Orlen", "Pavrik", "Quorra", "Rendall", "Sylra", "Torren",
    "Ulric", "Veyra", "Wendell", "Xarion", "Yelna", "Zorik", "Thess", "Brenna", "Cador", "Luneth",
    "Fenn", "Orel", "Syla", "Tharn", "Vindra", "Kalden", "Neris", "Thalia", "Garrik", "Melira",
    "Zanna", "Draven", "Isolde", "Varrin", "Soriel", "Korrin", "Aelric", "Tessia", "Branik", "Laziel",
    "Nerik", "Valen", "Soren", "Yvara", "Kestrel", "Oriax", "Delwyn", "Kira", "Jask", "Miriel",
    "Drevan", "Alira", "Taren", "Ossric", "Lenna", "Jaric", "Syvra", "Kellen", "Vestra", "Myric",
    "Zaren", "Brynn", "Calric", "Elaris", "Toric", "Haleen", "Selric", "Nimira", "Orrin", "Kaera",
    "Grath", "Velda", "Silar", "Dorel", "Malric", "Isen", "Nira", "Corwin", "Eris", "Lukan",
    "Vaela", "Rurik", "Zyra", "Kyran", "Aeris", "Thorne", "Liora", "Varen", "Mirek", "Saela"
    };

    private string[] descriptions = {
    "A grizzled veteran with a haunted past.",
    "A cheerful merchant always looking for a deal.",
    "A secretive mage with a mysterious agenda.",
    "A young noble running from their duties.",
    "An ex-bandit trying to turn their life around.",
    "A healer with a dark secret.",
    "A scholar obsessed with ancient ruins.",
    "A charming rogue who can’t be trusted.",
    "A devout priest questioning their faith.",
    "A retired adventurer with one last mission.",
    "A bard who knows everyone’s secrets.",
    "A quiet ranger tracking something unseen.",
    "An alchemist who’s always experimenting.",
    "A blacksmith with a fiery temper.",
    "A ship captain haunted by a sea beast.",
    "A farmer with unexpected magical talent.",
    "A traveling entertainer hiding from the law.",
    "A noble with a commoner’s heart.",
    "A grifter posing as royalty.",
    "A druid who rarely speaks to people.",
    "A war veteran who distrusts magic.",
    "A spy embedded in a local guild.",
    "A brewmaster with prophetic dreams.",
    "A librarian guarding a cursed book.",
    "A thief seeking redemption.",
    "An inventor with erratic ideas.",
    "A courier who sees too much.",
    "A cartographer searching for a lost city.",
    "A diplomat who’s lost all hope in peace.",
    "An outcast elf with a vendetta.",
    "A vampire pretending to be human.",
    "A bard cursed to speak only in rhyme.",
    "A sailor with stories no one believes.",
    "A former assassin turned pacifist.",
    "A goblin who dreams of being a hero.",
    "A beast-tamer with a pet wyvern.",
    "An oracle whose prophecies are too vague.",
    "A cowardly knight trying to be brave.",
    "A noble who talks to ghosts.",
    "A potion seller with memory loss.",
    "A sculptor possessed by a muse spirit.",
    "A painter who sees into other worlds.",
    "A gravekeeper who’s never alone.",
    "A hunter afraid of blood.",
    "A jester hiding a genius mind.",
    "A seer who can’t see their own fate.",
    "A soldier who hates violence.",
    "A tailor with magical thread.",
    "A brewer who talks to their beer.",
    "An archivist with perfect memory.",
    "A miner who digs for secrets.",
    "A mute locksmith with expressive eyes.",
    "A cursed baker whose bread screams.",
    "An archer who’s never missed — yet.",
    "A twin pretending to be their sibling.",
    "A child in an adult’s body.",
    "An old woman who never sleeps.",
    "A boy raised by trolls.",
    "A priest who lost faith during prayer.",
    "A hermit who hears whispers in stone.",
    "A monk seeking their lost twin flame.",
    "A sculptor shaping the future.",
    "An actor who forgot who they were.",
    "A puppetmaster controlling real people.",
    "A fae pretending to be human.",
    "A golem who thinks it's a person.",
    "A noble cursed with honesty.",
    "A scribe who writes people's futures.",
    "A glassblower haunted by fire spirits.",
    "A rogue poet who loves danger.",
    "An undead who doesn't know they're dead.",
    "A paladin in exile.",
    "A florist growing magical herbs.",
    "A child prophet with a gentle voice.",
    "A fisherman who caught a god.",
    "A jeweler who enchants gems by singing.",
    "A black cat in human form.",
    "A midwife with a secret twin.",
    "An herbalist with plant tattoos.",
    "A painter who paints events before they happen.",
    "A bookseller who remembers past lives.",
    "A blind storyteller who sees in dreams.",
    "A mailman delivering forbidden letters.",
    "A weaver whose cloth shows the future.",
    "A cook who flavors food with emotions.",
    "A beggar who once ruled a kingdom.",
    "A mask-maker who wears their own creations.",
    "A beekeeper with glowing honey.",
    "A potter sculpting from stardust.",
    "A mapmaker who charts dreams.",
    "A dancer who controls the wind.",
    "A glassmaker who traps souls in glass.",
    "A lantern-bearer who leads spirits home.",
    "A bellmaker whose chimes stop time.",
    "A gambler who always breaks even.",
    "A harpist whose music causes visions.",
    "A stonecutter carving future monuments.",
    "A candle maker who burns memories.",
    "A cloaked figure with no shadow.",
    "A moon-watcher who speaks to tides.",
    "A dreamer trapped between realms.",
    "A silent poet whose words appear in the air."
    };

    private string[] plotHooks = {
    "Is searching for a stolen heirloom.",
    "Is secretly working for a rival faction.",
    "Has information about a hidden dungeon.",
    "Wants revenge on a corrupt official.",
    "Is being blackmailed by a powerful figure.",
    "Needs help finding a missing sibling.",
    "Was the lone survivor of a doomed expedition.",
    "Is cursed to forget each day.",
    "Seeks a legendary weapon for a coming war.",
    "Believes the city is built over something ancient.",
    "Is hiding a fugitive from the authorities.",
    "Is being hunted by bounty hunters.",
    "Knows the weakness of a feared villain.",
    "Is trying to break a demonic pact.",
    "Carries a message meant for the king.",
    "Was part of a forgotten rebellion.",
    "Has visions of the players in danger.",
    "Is looking for someone to test a new potion.",
    "Must deliver a dangerous artifact in secret.",
    "Was recently bitten by a werewolf.",
    "Claims they met a god in the forest.",
    "Swears a dragon owes them a favor.",
    "Has a map that only shows up in moonlight.",
    "Seeks justice for a wrongful execution.",
    "Carries the key to a hidden vault.",
    "Wants to found a new settlement.",
    "Is being impersonated by a changeling.",
    "Has a child with latent magical power.",
    "Is trying to unite feuding families.",
    "Needs someone to translate a cursed scroll.",
    "Is secretly a prince/princess in hiding.",
    "Was tasked with guarding a forgotten ruin.",
    "Is being watched by an invisible creature.",
    "Woke up in a city with no memory.",
    "Is part of a prophecy they don’t believe in.",
    "Is trying to raise a magical beast in secret.",
    "Has stolen something they don’t understand.",
    "Is trying to break a bloodline curse.",
    "Is bound to serve anyone who beats them at chess.",
    "Knows of an ancient ritual about to be repeated.",
    "Was sent to spy, but fell in love.",
    "Is hiding a creature in their basement.",
    "Thinks they’re slowly turning into stone.",
    "Needs help completing a lifelong quest.",
    "Is the last survivor of a vanished village.",
    "Believes the moon speaks to them.",
    "Saw a god walking among mortals.",
    "Was visited by a future version of themselves.",
    "Knows the location of a forgotten temple.",
    "Claims a statue spoke to them.",
    "Found a relic and can't let go of it.",
    "Keeps reliving the same week.",
    "Received a vision of the world ending.",
    "Is haunted by a song they can’t forget.",
    "Keeps getting letters from the dead.",
    "Is building a machine that shouldn't work.",
    "Stole something that no one remembers existed.",
    "Saw a star fall and retrieved it.",
    "Is being tested by unknown forces.",
    "Was offered a wish and turned it down.",
    "Keeps finding the same coin every morning.",
    "Heard their name whispered in the wind.",
    "Discovered their blood opens hidden doors.",
    "Was saved by a mysterious stranger as a child.",
    "Believes their dreams are real memories.",
    "Was hired to follow the players.",
    "Needs help protecting a magical child.",
    "Lost something they can't describe.",
    "Is building an ark in the middle of nowhere.",
    "Is looking for a creature that only children see.",
    "Thinks their shadow is trying to kill them.",
    "Is trying to write the perfect lie.",
    "Is trapped in a magical contract.",
    "Has a mirror that shows only lies.",
    "Is aging backwards.",
    "Was told a secret that is killing them.",
    "Woke up with a strange tattoo.",
    "Is trying to resurrect a loved one.",
    "Is fleeing an arranged marriage.",
    "Has visions of distant planets.",
    "Is a key part of an ancient puzzle.",
    "Knows a spell that could end the war.",
    "Is a clone of someone powerful.",
    "Must reunite four ancient items.",
    "Was bitten by a cursed animal.",
    "Carries a vial they must never open.",
    "Is a double agent losing track of sides.",
    "Is building a sanctuary for monsters.",
    "Can hear thoughts when touching metal.",
    "Is immune to all magic but doesn’t know why.",
    "Can read any language, but only once.",
    "Believes they are in a dream.",
    "Was cursed to tell only the truth.",
    "Was born during a celestial event.",
    "Is building a monument to someone who doesn’t exist.",
    "Is haunted by the ghost of their future self.",
    "Was gifted an egg that never hatches.",
    "Knows the true name of a demon.",
    "Is seeking forgiveness from the gods.",
    "Trapped a soul in a gemstone — by accident.",
    "Seeks to undo a wish they once made."
    };

    private string[] occupation = {
    "Blacksmith", "Innkeeper", "Hunter", "Thief", "Assassin", "Scribe", "Cartographer", "Guard", "Bard", "Merchant",
    "Alchemist", "Priest", "Fisherman", "Sailor", "Captain", "Druid", "Sorcerer", "Warlock", "Wizard", "Knight",
    "Noble", "Servant", "Librarian", "Farmer", "Baker", "Cook", "Brewer", "Stablemaster", "Scholar", "Historian",
    "Healer", "Gravedigger", "Fletcher", "Armorer", "Leatherworker", "Miner", "Herbalist", "Scout", "Messenger", "Diplomat",
    "Seer", "Engineer", "Inventor", "Tailor", "Jeweler", "Glassblower", "Painter", "Musician", "Beggar", "Street Performer",
    "Spy", "Informant", "Witch", "Beast Tamer", "Warden", "Executioner", "Monk", "Courier", "Gambler", "Smuggler",
    "Barber", "Tavern Bouncer", "Watchman", "Mayor", "Pickpocket", "Butcher", "Bookbinder", "Candlemaker", "Gatekeeper", "Tax Collector",
    "Hunter-for-hire", "Mercenary", "Bodyguard", "Spy Hunter", "Prophet", "Cult Leader", "Fortune Teller", "Archivist", "Clockmaker", "Plague Doctor",
    "Dungeon Cleaner", "Rune Engraver", "Potion Brewer", "Scroll Seller", "Beekeeper", "Map Seller", "Antique Dealer", "Sewer Worker", "Torchbearer", "Treasure Diver",
    "Exorcist", "Beast Hunter", "Wagon Driver", "Lantern Maker", "Oracle", "Crypt Keeper", "Relic Collector", "Fossil Digger", "Monster Trainer", "Cursed Item Broker"
};

    private string[] appearance = {
    "Tall and lanky, with sunken cheeks", "Short and round, with rosy cheeks", "Piercing green eyes and a crooked nose", "Bald with intricate tattoos", "Wears an eyepatch over a glowing eye",
    "Always wears a hood, even indoors", "Burn scars cover one side of the face", "Hair like woven silver threads", "Skin marked with strange runes", "Missing a hand, replaced by a mechanical claw",
    "Wears mismatched armor pieces", "Constantly covered in soot and ash", "Walks with a limp", "Eyes that never blink", "One arm far more muscular than the other",
    "Always surrounded by bees", "Smells strongly of herbs", "Cloak seems to move on its own", "Hair floats slightly as if underwater", "Voice echoes unnaturally",
    "Wears a necklace of monster teeth", "Eyes glow in the dark", "Never casts a shadow", "Wears a mask with no mouth", "Has a third eye tattooed on forehead",
    "Always covered in bloodstains", "Missing an ear", "Has tusks instead of teeth", "Wears glasses too small for their head", "Walks barefoot, no matter the weather",
    "Dresses like royalty despite shabby manners", "Covered in feathers", "Antlers growing from their skull", "Has a deep scar across their chest", "Never speaks, only gestures",
    "Hair changes color daily", "One eye is reptilian", "Smokes a constantly burning pipe", "Wears seven rings on each hand", "Clothes stitched from different banners",
    "Constantly twitching", "Covered in chalk dust", "Fingers stained with ink", "Wears bones as jewelry", "Smells of ocean brine", "Eyes like cracked glass",
    "Always wet, like just out of the rain", "Voice sounds like two people speaking", "Floats a few inches off the ground", "Wears a helm they never remove", "Looks completely average — unnervingly so",
    "Hair woven with bones", "Carries a staff taller than themselves", "Wears nothing but a scarf", "Face obscured by long hair", "Stares too long when spoken to",
    "Carries live rats in pockets", "Has tree bark for skin", "Has one glowing hand", "Has tiny horns", "Teeth filed into points",
    "Speaks through a puppet", "Always carries a book", "Has a tail, hidden poorly", "Face always in shadow", "Eyes of two different colors",
    "Tattoo of a map across their arm", "Wears a collar with a lock", "Covered in bite marks", "Has no fingernails", "Dresses like a priest but curses like a sailor",
    "Cries blood when angry", "Wears a cloak of shifting stars", "Voice only audible to some", "Wears mismatched shoes", "Hair braided with metal wire",
    "Eyes roll back when they speak", "Bleeds blue", "Wears a porcelain mask", "Teeth too many for one mouth", "Hollow-sounding footsteps",
    "Always whistling", "Shadow moves on its own", "Mouth stitched shut — but still speaks", "Hair smokes when cut", "Back hunched unnaturally",
    "Limbs too long for their body", "Walks backwards", "Leaves ash footprints", "Skin like cracked earth", "Carries a child’s doll",
    "Carries a torch even in daylight", "Breath always visible", "Smells faintly of cinnamon", "Wears gloves at all times", "Face slowly changing",
    "Wears eyes around their belt", "Scarred with claw marks", "One eye covered with gold leaf", "Laughs randomly", "Eyes look in different directions"
};

    private string[] personality = {
    "Grumpy but good-hearted", "Suspicious of everyone", "Always joking, even in danger", "Obsessed with cleanliness", "Talks to themselves constantly",
    "Overly formal", "Blunt and brutally honest", "Quiet and observant", "Always lying, even when unnecessary", "Kind but forgetful",
    "Paranoid and twitchy", "Charismatic and manipulative", "Emotionless and monotone", "Excitable and loud", "Inquisitive to a fault",
    "Deeply religious", "Cold and calculating", "Easily frightened", "Always bored", "Talks in riddles",
    "Friendly and outgoing", "Loyal to a fault", "Quick to anger", "Always apologizing", "Obsessed with death",
    "Loves to gamble", "Constantly singing", "Lacks empathy", "Wants to be liked", "Always daydreaming",
    "Clingy and overly trusting", "Suspicious of magic", "Loves to argue", "Intensely curious", "Cannot keep a secret",
    "Greedy and selfish", "Generous to a fault", "Naïve and hopeful", "Sarcastic and dry", "Believes in destiny",
    "Adores children", "Despises authority", "Hates violence", "Seeks revenge", "Always quoting scriptures",
    "Acts braver than they are", "Unshakably calm", "Laughs at everything", "Treats everyone like royalty", "Believes the world is a test",
    "Fears being alone", "Loves being the center of attention", "Highly competitive", "Often lost in thought", "Terrified of the dark",
    "Has no sense of humor", "Overconfident", "Secretly very lonely", "Speaks in third person", "Tends to whisper everything",
    "Laughs when nervous", "Overthinks every decision", "Wants to write a book", "Tries to act mysterious", "Extremely literal",
    "Loves nature", "Distrusts all strangers", "Always hungry", "Wants to be a hero", "Hates their job",
    "Romantic and dreamy", "Doesn’t believe in magic", "Takes everything personally", "Wants to impress", "Always late",
    "Talks to animals", "Lives in the past", "Forgets names instantly", "Hates rules", "Thinks they’re cursed",
    "Addicted to storytelling", "Has a code of honor", "Wants to be feared", "Can’t say no", "Fears aging",
    "Loves puzzles", "Hums constantly", "Wants to see the world burn", "Values knowledge above all", "Desperately seeks approval",
    "Judges others harshly", "Always planning ahead", "Believes in fate", "Lies to protect others", "Fears being forgotten",
    "Loves shiny things", "Desires a quiet life", "Feels superior", "Is tired of everything", "Speaks in rhymes"
};

    private string[] inventory = {
    "A rusted dagger", "A pouch of strange coins", "A broken compass", "An enchanted locket", "A vial of glowing liquid",
    "A rolled-up treasure map", "A journal with missing pages", "A severed goblin finger", "A ring that hums softly", "A cursed mirror shard",
    "A deck of marked cards", "A silver whistle", "A bag of herbs", "A lockpick set", "A glowing gemstone",
    "A cracked monocle", "A feather from a phoenix", "A vial of vampire blood", "A dried frog", "A candle that won't go out",
    "A cursed coin", "A whistle only animals hear", "A small bag of teeth", "A compass that points to danger", "A bone flute",
    "A stone that absorbs light", "A shrunken head", "A bottle of invisible ink", "A mechanical eye", "A mirror shard showing another world",
    "A scroll in an unknown language", "A wooden idol", "A dried rose", "A finger bone", "A jar of eyeballs",
    "A lantern that burns cold fire", "A silver key with no lock", "A box of ashes", "A vial of shadow", "A beetle in amber",
    "A ring engraved with runes", "A flute that causes sleep", "A cursed child’s toy", "A crown made of thorns", "A piece of a broken crown",
    "A shard of obsidian", "A pouch of ghost dust", "A melted silver spoon", "A locket with moving images", "A book with no words",
    "A set of bone dice", "A tiny coffin", "A claw from a griffon", "A spider preserved in honey", "A bloodstained handkerchief",
    "A needle that stitches by itself", "A rope that ties itself", "A small hourglass", "A vial labeled 'Do Not Drink'", "A fang wrapped in silk",
    "A weathered wanted poster", "A bag of black sand", "A tooth with a rune", "A small talking skull", "A pouch that meows",
    "A crystal skull", "A preserved bat wing", "A music box with no tune", "A coin that changes faces", "A rune-covered rock",
    "A vial of troll spit", "A piece of a shattered mask", "A tiny spellbook", "A cursed chess piece", "A black feather",
    "A faded photograph", "A quill that writes alone", "A goblet that refills randomly", "A withered hand", "A charm against demons",
    "A broken wand", "A whispering crystal", "A dried snake curled in a bottle", "A hairbrush with no bristles", "A brass monocle with three lenses",
    "A cracked hourglass", "A mirror that shows your fears", "A tooth wrapped in red thread", "A pouch of glowing moss", "A whistle shaped like a skull",
    "A key that screams when used", "A chain made of silver bones", "A flask that never empties", "A candle that drips black wax", "A pipe that sings",
    "A nail from a coffin", "A wooden box with a beating heart", "A skull with jeweled eyes", "A glass eye", "A bag of cursed dice"
};

    private string[] quote = {
    "The dead speak, if you know how to listen.", "Never trust a smiling wizard.", "I’ve seen the end, and it’s not pretty.",
    "Gold won’t save you from the shadows.", "Everyone has a secret. Even you.", "The stars are wrong tonight.", "I only steal from those who deserve it.",
    "I didn’t kill them. Not all of them, anyway.", "You remind me of someone I buried.", "This town reeks of old magic.",
    "Coin talks louder than kings.", "The truth is buried in blood.", "It’s not paranoia if they’re really after you.",
    "I once kissed a demon. She still writes me.", "Some doors are meant to stay closed.", "I owe a debt to the sea.",
    "The forest knows your name.", "They never find the bodies.", "I see things others can’t.", "There’s poison in the wine.",
    "The gods forgot us long ago.", "I don't fear death, just the things after.", "You’re not from around here, are you?", "I know what you are.",
    "This blade remembers every soul it’s taken.", "They say I’m cursed. They’re not wrong.", "Never light a fire in the Whispering Woods.",
    "Even my lies are half-true.", "My shadow has its own opinions.", "The moon told me you'd come.", "I warned them. They didn’t listen.",
    "I've been dead before.", "Don’t speak the name. Not here.", "They say the king is a puppet. I say he's the least dangerous one.",
    "One step closer and you’ll meet your god.", "The silence in the graveyard isn’t natural.", "I sleep with one eye open. The other doesn't close.",
    "This town eats people alive.", "You can’t outrun your blood.", "Not all chains are made of metal.", "The wind carries screams, if you listen close.",
    "I’ve read your fate. It's… messy.", "Careful where you tread. This ground remembers.", "I’ve seen your kind before. They didn't last long.",
    "I’ve killed for less.", "The rats told me everything.", "You're not the first to look for answers here.", "This ring? It belonged to a god.",
    "Don’t drink the well water.", "The fire told me your name.", "Nothing bleeds like a liar.", "I keep secrets — mostly my own.",
    "You carry more than you know.", "You can’t kill what doesn’t live.", "I walk between moments.", "Even death fears the dark.",
    "I dream of teeth.", "That wasn’t thunder.", "You’ll hear the bell when it’s too late.", "The sky is wrong today.",
    "Everything has a price. Especially mercy.", "You brought a sword to a whisper fight?", "My luck ran out, so I borrowed yours.",
    "I’d stay away from the shadows if I were you.", "I don’t bleed like I used to.", "You can’t save them. I tried.",
    "That scream? It’s normal around here.", "No gods, no masters. Just me.", "You’re asking the wrong questions.",
    "I remember when this place was alive.", "They said I’d never leave. They were right.", "The mirror doesn’t show me anymore.",
    "I speak to walls. Sometimes, they reply.", "The bones still move at night.", "You hear that? Me neither.",
    "I was once like you — hopeful.", "I’d run if I were you. Now.", "You dream in color? Lucky you.",
    "Sometimes the eyes lie.", "It’s the third knife that kills.", "I have names for all my scars.",
    "You don’t find this town. It finds you.", "I'm not lost — I'm hiding.", "Silence is the loudest warning.",
    "You’d be surprised what people pay for a secret.", "They only burn witches they fear.", "I’m not from here, but neither are you.",
    "I forgot my name to forget my crimes.", "Even my shadow runs from this place.", "I’m only dangerous when I’m smiling.",
    "Ask the right question and the world changes.", "I left my conscience in the swamp.", "I trade in memories.",
    "Your heartbeat is too loud.", "I once loved something. It didn't end well.", "This blade feeds on guilt.",
    "Every scar has a story. Some scream louder than others.", "I gave my soul for a second chance.", "That smell? That's fate burning."
};

    private string[] backstory = {
    "Abandoned as a child and raised by wolves.", "Once a noble, now a fugitive.", "Cursed after stealing from a tomb.",
    "Served as a war medic in a losing army.", "Escaped a cult that summoned something terrible.", "Was the lone survivor of a shipwreck.",
    "Trained by assassins, betrayed by their master.", "Imprisoned for a crime they didn’t commit — or so they claim.",
    "Haunted by the ghost of a sibling.", "Born under a blood moon prophecy.", "Raised in a traveling circus of monsters.",
    "Worked as a gravedigger to pay off a demon’s debt.", "Once the apprentice to a mad alchemist.", "Grew up in the slums of a cursed city.",
    "Banished from a wizard’s college for forbidden studies.", "Fought in a rebellion that failed.", "Raised by an order of silent monks.",
    "Used to hunt beasts for coin — until one bit back.", "Sailed with pirates across the screaming seas.", "Was once royalty, now unrecognizable.",
    "Trained in a forgotten martial art.", "Escaped from an underwater city.", "Once a gladiator forced to fight siblings.",
    "Belonged to a nomadic people wiped out by war.", "Witnessed a god fall from the sky.", "Built weapons for both sides of a war.",
    "Served a vampire lord, then fled at dawn.", "Lived in a library older than kingdoms.", "Broke an ancient oath for love.",
    "Touched a cursed relic and lived.", "Lured a dragon to a village by mistake.", "Raised by druids in an everchanging forest.",
    "Was buried alive — and crawled out.", "Once possessed, still hears whispers.", "Stole a name from a dying man.",
    "Learned to fight in the pits of a ruined city.", "Smuggled relics out of fallen temples.", "Failed to protect someone important.",
    "Studied under a lich, still has the notes.", "Slept in stone for a hundred years.", "Once traded their reflection for power.",
    "Sailed with sirens and survived.", "Accidentally started a plague.", "Chased a dream into another realm — and came back wrong.",
    "Came from a line of monster hunters — now the last.", "Watched their town disappear in one night.", "Broke reality and barely put it back.",
    "Lived through five lifetimes, none happy.", "Was the favorite of a forgotten god.", "Failed a summoning that consumed a city.",
    "Fled from a marriage to a demon.", "Once died and made a deal to return.", "Walked with titans in their dreams.",
    "Wrote a prophecy they now fear.", "Used to serve in the royal guard — until the coup.", "Once held a crown for ten minutes.",
    "Knows the true name of a star.", "Learned magic from whispers in a well.", "Sacrificed something important for survival.",
    "Was traded to fey at birth.", "Hunted their own kind to survive.", "Once turned to stone for a decade.",
    "Saw their future and tried to change it.", "Followed a ghost across continents.", "Built a mechanical heart after theirs failed.",
    "Protected a cursed child.", "Lost everything to one bad deal.", "Once stole fire from the gods.",
    "Carried the ashes of a lost kingdom.", "Escaped a prison beneath the sea.", "Used to be famous — now forgotten.",
    "Grew up believing they were someone else.", "Betrayed their order to save a stranger.", "Danced with death and won — barely.",
    "Became a folk tale, then returned.", "Survived a cursed winter alone.", "Raised in a graveyard by a kind necromancer.",
    "Once ate something they shouldn't have — and changed.", "Was trained as a decoy double.", "Loved someone forbidden.",
    "Spoke with a dead god in a dream.", "Lived in the belly of a beast for weeks.", "Once part of a cult, now hunts them.",
    "Survived by pretending to be someone else.", "Lived with ghosts for a decade.", "Trained to be the next prophet.",
    "Carried a crown meant for another.", "Once turned into a raven — still remembers flying.", "Forgot everything after a lightning strike.",
    "Crossed a continent chasing a lie.", "Once gave birth to a shadow.", "Searched the desert for a voice.",
    "Watched a kingdom fall from inside the walls.", "Sold their memories to pay a debt.", "Learned everything from a talking sword.",
    "Guided the dead to the afterlife — once.", "Saved a monster as a child.", "Was once a statue in a forgotten temple.",
    "Knows they’re part of someone’s story.", "Broke into heaven. Wasn’t impressed."
};

    public void GenerateNPC()
    {
        string newName = names[Random.Range(0, names.Length)];
        string newDescription = descriptions[Random.Range(0, descriptions.Length)];
        string newPlotHook = plotHooks[Random.Range(0, plotHooks.Length)];
        string newOccupation = plotHooks[Random.Range(0, occupation.Length)];
        string newAppearance = plotHooks[Random.Range(0, appearance.Length)];
        string newPersonality = plotHooks[Random.Range(0, personality.Length)];
        string newInventory = plotHooks[Random.Range(0, inventory.Length)];
        string newQuote = plotHooks[Random.Range(0, quote.Length)];
        string newBackStory = plotHooks[Random.Range(0, backstory.Length)];

        // Generate random stats
        string newStats = GenerateRandomStats();

        nameField.text = newName;
        descriptionField.text = newDescription;
        plotHookField.text = newPlotHook;
        statsField.text = newStats;
        occupationField.text = newOccupation;
        appearanceField.text = newAppearance;
        personalityField.text = newPersonality;
        inventoryField.text = newInventory;
        quoteField.text = newQuote;
        backStoryField.text = newBackStory;

        Debug.Log($"Generated NPC: {newName}, {newDescription}, Hook: {newPlotHook}, Stats: {newStats}");
    }

    private string GenerateRandomStats()
    {
        string leftStats = "";
        string rightStats = "";

        for (int i = 0; i < statNames.Length; i++)
        {
            int randomValue = Random.Range(8, 21); // Stats between 8 and 20
            string statLine = $"{statNames[i]}: {randomValue}";

            if (i < 3) // First 3 stats go to the left side
            {
                leftStats += statLine.PadRight(25); // Align to the left with padding
            }
            else // Last 3 stats go to the right side
            {
                rightStats += statLine.PadRight(25); // Align to the left with padding
            }
        }

        // Format as two columns, separated by a newline
        return $"{leftStats}\n{rightStats}";
    }

    public void CopyToClipboard()
    {
        string npcText = $"Name: {nameField.text}\nDescription: {descriptionField.text}\nPlot Hook: {plotHookField.text}\nStats:\n{statsField.text}";
        GUIUtility.systemCopyBuffer = npcText;
        Debug.Log("NPC copied to clipboard!");
    }

    public void DownloadToTxt()
    {
        string npcText = $"Name: {nameField.text}\nDescription: {descriptionField.text}\nPlot Hook: {plotHookField.text}\nStats:\n{statsField.text}";
        string path = Path.Combine(Application.persistentDataPath, "GeneratedNPC.txt");

        File.WriteAllText(path, npcText);
        Debug.Log($"NPC saved to {path}");
    }
}
