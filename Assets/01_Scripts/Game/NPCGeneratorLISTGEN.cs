using UnityEngine;
using TMPro;
using System.IO;

public class NPCGeneratorLISTGEN : MonoBehaviour
{
    public TMP_InputField nameField;
    public TMP_InputField descriptionField;
    public TMP_InputField plotHookField;
    public TMP_InputField statsField;

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


    public void GenerateNPC()
    {
        string newName = names[Random.Range(0, names.Length)];
        string newDescription = descriptions[Random.Range(0, descriptions.Length)];
        string newPlotHook = plotHooks[Random.Range(0, plotHooks.Length)];

        // Generate random stats
        string stats = GenerateRandomStats();

        nameField.text = newName;
        descriptionField.text = newDescription;
        plotHookField.text = newPlotHook;
        statsField.text = stats;

        Debug.Log($"Generated NPC: {newName}, {newDescription}, Hook: {newPlotHook}, Stats: {stats}");
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
