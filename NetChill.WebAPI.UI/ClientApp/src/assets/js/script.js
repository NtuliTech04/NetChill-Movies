//Regex Expressions
const numericsRegex = /[0-9]+$/gm;
const properCaseRegex = /^[A-Z][a-z]*(\s[A-Z][a-z]*)*$/;
const titleCaseRegex = /^[A-Z0-9][A-Za-z0-9]*(\s[A-Z0-9][A-Za-z0-9]*)*$/; //Incl. Numbers
const sentenceCaseRegex = /^[A-Z][a-zA-Z0-9',. -:;?"]*(\s[a-zA-Z0-9',. -:;?"]+)*$/;