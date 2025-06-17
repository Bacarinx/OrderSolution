import js from "@eslint/js";
import eslintImport from "eslint-plugin-import";
import jsxA11y from "eslint-plugin-jsx-a11y";
import prettier from "eslint-plugin-prettier";
import react from "eslint-plugin-react";
import reactHooks from "eslint-plugin-react-hooks";
import globals from "globals";

export default [
  {
    ignores: ["dist", "node_modules"],
  },
  {
    files: ["**/*.{js,jsx}"],
    languageOptions: {
      ecmaVersion: "latest",
      sourceType: "module",
      globals: {
        ...globals.browser,
        ...globals.es2021,
      },
      parserOptions: {
        ecmaFeatures: {
          jsx: true,
        },
      },
    },
    plugins: {
      react,
      "react-hooks": reactHooks,
      "jsx-a11y": jsxA11y,
      import: eslintImport,
      prettier,
    },
    rules: {
      ...js.configs.recommended.rules,
      ...react.configs.recommended.rules,
      ...reactHooks.configs.recommended.rules,
      ...jsxA11y.configs.recommended.rules,
      ...eslintImport.configs.recommended.rules,

      // Regras extras usadas pela Rocketseat
      "prettier/prettier": "error",
      "react/jsx-uses-react": "off",
      "react/react-in-jsx-scope": "off",
      "import/order": [
        "error",
        {
          groups: ["builtin", "external", "internal"],
          alphabetize: { order: "asc", caseInsensitive: true },
        },
      ],
      "no-unused-vars": [
        "warn",
        { argsIgnorePattern: "^_", varsIgnorePattern: "^_" },
      ],
    },
    settings: {
      react: {
        version: "detect",
      },
    },
  },
];
