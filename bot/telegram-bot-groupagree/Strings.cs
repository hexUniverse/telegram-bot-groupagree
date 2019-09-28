﻿using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using WJClubBotFrame;
using System.Linq;

namespace telegrambotgroupagree {
	public class Strings {
		public enum Langs {
			none,
			en,
			de,
			it,
			ptBR, //pt-BR
			he,
			uk,
			nb_NO, //nb-NO
			fa,
			sp, //es
			zh_TW, //zh-TW
			zh_HK, //zh-HK
			zh_CN, //zh-CN
			rus, //ru
			fr,
			uz,
		};

		public static Dictionary<string, Langs> IeftLookupTable = new Dictionary<string, Langs> {
			{ "C" , Langs.none},
			{ "en" , Langs.en },
			{ "de" , Langs.de },
			{ "it" , Langs.it },
			{ "pt-BR" , Langs.ptBR },
			{ "he" , Langs.he },
			{ "uk" , Langs.uk },
			{ "nb-NO" , Langs.nb_NO },
			{ "fa" , Langs.fa },
			{ "sp" , Langs.sp },
			{ "zh-TW" , Langs.zh_TW },
			{ "zh-HK" , Langs.zh_HK },
			{ "zh-CN" , Langs.zh_CN },
			{ "ru" , Langs.rus },
			{ "fr" , Langs.fr },
			{ "uz" , Langs.uz },
		};
		
		public enum StringsList {
			nothingToCancel,
			canceledThat,
			hasCanceled,
			creatingPoll,
			donation2,
			setupPlease,
			boardAnswer,
			pollDoesntExist,
			unrecognizedCommand,
			wannaCreate,
			createPoll,
			createBoard,
			addedToPoll,
			alreadyDefined,
			done,
			boardSuccess,
			tooManyCharacters,
			inlineTextCreateNewPoll,
			finishedPollCreation,
			noPollsToFinish,
			seriouslyWannaDeleteThePoll,
			sure,
			no,
			roger,
			rogerSendIt,
			inlineButtonVote,
			inlineButtonDoodle,
			inlineButtonBoard,
			callbackVotedFor,
			voteDoesntExist,
			startMessage,
			startMessageChangePollTypeButton,
			startMessagePersonalButton,
			startMessageAnonyButton,
			votedSuccessfully,
			callbackTookBack,
			listYourPolls,
			listNothingHere,
			vote,
			doodle,
			board,
			limitedDoodle,
			rendererMultiVotedSoFar,
			rendererSingleVotedSoFar,
			rendererZeroVotedSoFar,
			pollClosed,
			boardPagCantFind,
			boardPagBottomLine,
			boardShowMore,
			buttonVote,
			setLanguage,
			updatingPoll,
			skipThisStep,
			publish,
			publishWithLink,
			commPageClose,
			commPageReopen,
			commPageDelete,
			commPageRefresh,
			tryVotePollClosed,
			callbackLimitedMaxReached,
			limitedDoodleYouCanChooseSoMany,
			limitedDoodleEnterMaxVotes,
			limitedDoodleGiveMeANumberNothingElse,
			limitedDoodleGiveMeANumberSmallerOrEqualToOptionCount,
			inlineButtonLimitedDoodle,
			pollNotFound,
			seriouslyDeleteEverything,
			deletedEverything,
			sadlyEverythingThere,
			seriouslySureDeleteEverything,
			deleteAllFooter,
			commPageOptions,
			optionsForPoll,
			optionsPercentageNone,
			optionsSorted,
			optionsUnsorted,
			optionsAppendable,
			optionsUnappendable,
			optionsShareable,
			buttonAppend,
			appendSendMe,
			addOptionSuccess,
			addOptionTooManyChars,
			moderate,
			pollBeingEdited,
			inlineDescriptionPersonalVote,
			inlineDescriptionAnonymousVote,
			inlineDescriptionPersonalDoodle,
			inlineDescriptionAnonymousDoodle,
			inlineDescriptionPersonalLimitedDoodle,
			inlineDescriptionAnonymousLimitedDoodle,
			inlineDescriptionPersonalBoard,
			inlineDescriptionAnonymousBoard,
			viewInBrowserExpansion,
			inlineDescriptionFirstLine,
			pollAlreadyDeleted,
			optionNotFound,
			optionDeleteSuccess,
			optionDeleteNonSuccess,
			dontMessWithTheCommands,
			pollOptionDeleteErrorButPollStillFound,
			aPollHasToHaveAtLeastOneOption,
			optionAlreadyExists,
			creatingPollWannaAddDescription,
			addDescription,
			sendPollDescription,
            clone,
            creatingBoardWannaAddDescription,
			addInstanceSendToken,
			addInstanceSetParameters,
			addInstanceParameterList,
			addInstanceToQueueButton,
			addInstanceBePatientText,
			addInstanceApproved,
			addInstanceRejected,
			addInstanceTechnicalIssue,
			pollDeleted,
			pollOptionDeleteBoardHasChanged,
			pollOptionDeleteBoardVoteYesDelete,
			pollOptionDeleteBoardVoteNoKeep,
			pollTypeDescription,
			tooManyRequestsByUser,
			tooManyRequestsForPoll,
			messageIDInvalidWelcomeMessage,
			tooManyRequestsForMessage,
			caughtScrewingWithCallbacks,
		};


		private Langs currentLang = Langs.en;
		public Langs CurrentLang { get { return this.currentLang; } }

		Dictionary<Langs, Dictionary<StringsList, string>> langStrings;
		public Dictionary<StringsList, string> StringsEn { get { return langStrings[Langs.en]; } }
		Dictionary<Langs, string> langNames;

		public Strings() {
			string stringsFile = "";
			try {
				using (StreamReader reader = new StreamReader(@"strings.json")) {
					stringsFile += reader.ReadToEnd();
				}
			} catch (FileNotFoundException) {
				//TODO Do something
			}
			try {
				langStrings = JsonConvert.DeserializeObject<Dictionary<Langs, Dictionary<StringsList, string>>>(stringsFile);
			} catch (Newtonsoft.Json.JsonSerializationException) {
				Notifications.log("You fucked up the language files...");
				throw;
			}
			string langNamesFile = "";
			try {
				using (StreamReader reader = new StreamReader(@"langnames.json")) {
					langNamesFile += reader.ReadToEnd();
				}
			} catch (FileNotFoundException) {

			}
			langNames = JsonConvert.DeserializeObject<Dictionary<Langs, string>>(langNamesFile);
		}

		public void SetLanguage(Langs lang) {
			this.currentLang = lang;
		}

		internal string GetLangName(Langs lang) {
			return langNames[lang];
		}

		public string GetString(StringsList name) {
			try {
				return langStrings[(currentLang == Langs.none ? Langs.zh_TW : currentLang)][name];
			} catch (System.Collections.Generic.KeyNotFoundException) {
				/*#if DEBUG
				Notifications.log(string.Format("I couldn't find {1} in strings {0}", currentLang.ToString(), name.ToString()));
				#endif*/
				return langStrings[Langs.zh_TW][name];
			}
		}

		public Dictionary<Langs, Dictionary<StringsList, string>> GetMissingStrings() {
			Dictionary<Langs, Dictionary<StringsList, string>> output = new Dictionary<Langs, Dictionary<StringsList, string>>();
			for (int i = 1; i <= langNames.Count; i++) {
				output.Add((Langs)i, new Dictionary<StringsList, string>());
				for (int j = 0; j < Enum.GetNames(typeof(StringsList)).Length; j++) {
					try {
						string temp = langStrings[(Langs)i][(StringsList)j];
					} catch (System.Collections.Generic.KeyNotFoundException) {
						output[(Langs)i].Add((StringsList)j, this.GetString((StringsList)j));
					}
				}
			}
			return output;
		}

		public static Langs GetLangFromIEFT(string ieftTag) {
			Langs outputLang = Langs.none;
			if (!IeftLookupTable.TryGetValue(ieftTag, out outputLang))
				if (IeftLookupTable.Any(x => x.Key.StartsWith(ieftTag.Substring(0,2))))
					outputLang = IeftLookupTable.First(x => x.Key.StartsWith(ieftTag.Substring(0,2))).Value;
			return outputLang;
		}
	}
}