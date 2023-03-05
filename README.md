System semi auto-dock
---
Pour les InGameScript
> Gestion partiel de amarage des ship petit bloc

*Dans les grande lignes, le script va gére:*
>- le script va connecter le vaisseau
>- mettre les batteries en Recharger
>- les tanks H2 en stockpile ON
>- et les thrusters en OFF
>- Puis vice-versa pour déconnexion,
>- Avec un timer(uniquement pour la déco) réglable en début de script.

***ATTENTION***: actuellement le ship doit être en petit bloc avec un seul et unique connecteur.

Dans le jeu:

1- Un ship avec **UN** connecteur.

2- Un Programmable Block (PB) sur le vaisseau

3- Coller le script dans le PB(le script se lance et vous informe s'il a bien trouvé son connecteur)

4- Menu G dans le ship, glisser le PB dans la bar de raccoucis en utilisant option RUN

5- Donner l'argument "co" pour la connection,

6- Répéter l'étape 4 mais sur un autre emplacement.

7- Donner l'argument "deco" pour la déconnection.

[github version lite](https://github.com/aangee/AutoDocV1Light)
