using backend.Data;
using backend.Models;
using BCrypt.Net;

namespace backend.Data
{
    public static class SeedData
    {
        public static void Initialize(AppDbContext db)
        {
            // --- USERS ---
            if (!db.Users.Any(u => u.Id > 1))
            {
                var passwordHash = BCrypt.Net.BCrypt.HashPassword("admin123");

                db.Users.AddRange(
                    new User { Id = 2, FullName = "Ahmed Kamel", Email = "ahmed.kamel@simsoft.tn", PasswordHash = passwordHash, Role = "Admin", CreatedAt = DateTime.UtcNow.AddMonths(-5), LastLogin = DateTime.UtcNow.AddDays(-1), IsActive = true },
                    new User { Id = 3, FullName = "Sara Mansouri", Email = "sara.mansouri@simsoft.tn", PasswordHash = passwordHash, Role = "Admin", CreatedAt = DateTime.UtcNow.AddMonths(-5), LastLogin = DateTime.UtcNow.AddDays(-3), IsActive = true },
                    new User { Id = 4, FullName = "Youssef Trabelsi", Email = "y.trabelsi@simsoft.tn", PasswordHash = passwordHash, Role = "SuperAdmin", CreatedAt = DateTime.UtcNow.AddMonths(-4), LastLogin = DateTime.UtcNow.AddHours(-6), IsActive = true },
                    new User { Id = 5, FullName = "Leila Gharbi", Email = "l.gharbi@simsoft.tn", PasswordHash = passwordHash, Role = "Admin", CreatedAt = DateTime.UtcNow.AddMonths(-4), LastLogin = DateTime.UtcNow.AddDays(-7), IsActive = true },
                    new User { Id = 6, FullName = "Karim Bouzid", Email = "k.bouzid@simsoft.tn", PasswordHash = passwordHash, Role = "Admin", CreatedAt = DateTime.UtcNow.AddMonths(-3), LastLogin = DateTime.UtcNow.AddDays(-15), IsActive = true },
                    new User { Id = 7, FullName = "Nadia Ferchichi", Email = "n.ferchichi@simsoft.tn", PasswordHash = passwordHash, Role = "Admin", CreatedAt = DateTime.UtcNow.AddMonths(-2), LastLogin = DateTime.UtcNow.AddDays(-30), IsActive = false },
                    new User { Id = 8, FullName = "Amine Bouaziz", Email = "a.bouaziz@simsoft.tn", PasswordHash = passwordHash, Role = "Admin", CreatedAt = DateTime.UtcNow.AddMonths(-1), LastLogin = null, IsActive = false }
                );
                db.SaveChanges();
            }

            // --- BLOG POSTS ---
            if (!db.BlogPosts.Any())
            {
                var rng = new Random(42);
                var authors = new[] { "Ahmed Kamel", "Sara Mansouri", "Youssef Trabelsi", "Leila Gharbi", "Karim Bouzid" };
                var images = new[]
                {
                    "https://images.unsplash.com/photo-1518770660439-4636190af475?w=800",
                    "https://images.unsplash.com/photo-1581091226825-a6a2a5aee158?w=800",
                    "https://images.unsplash.com/photo-1498050108023-c5249f4df085?w=800",
                    "https://images.unsplash.com/photo-1563986768609-322da13575f3?w=800",
                    "https://images.unsplash.com/photo-1504384308090-c894fdcc538d?w=800",
                    "https://images.unsplash.com/photo-1558494949-ef010cbdcc31?w=800",
                    "https://images.unsplash.com/photo-1460925895917-afdab827c52f?w=800",
                };

                var posts = new List<BlogPost>
                {
                    // ERP Industriel (8)
                    new BlogPost { Id = 1, Title = "Comment SimSoft ERP transforme la gestion de production industrielle", Content = "Dans un contexte de concurrence mondiale accrue, les entreprises industrielles doivent s'appuyer sur des outils numériques performants. SimSoft ERP offre une visibilité complète sur les flux de production, de la matière première au produit fini. Grâce à une intégration native avec les machines de l'atelier (via OPC-UA), les données remontent en temps réel dans le système, éliminant les saisies manuelles et les erreurs associées. Les responsables de production disposent ainsi d'indicateurs clés (TRS, taux de rebut, délais de fabrication) accessibles depuis n'importe quel appareil.", Category = "ERP Industriel", Author = authors[0], ImageUrl = images[0], IsPublished = true, CreatedAt = DateTime.UtcNow.AddDays(-rng.Next(30, 180)) },
                    new BlogPost { Id = 2, Title = "Les 5 signes que votre ERP actuel freine la croissance de votre entreprise", Content = "Beaucoup d'entreprises industrielles fonctionnent encore avec des ERP conçus dans les années 2000, inadaptés aux enjeux modernes. Voici les 5 signaux d'alarme : des délais de clôture comptable trop longs, une impossibilité de tracer les lots en temps réel, des exports Excel permanents pour contourner le système, une impossibilité d'accéder aux données depuis le terrain, et l'absence de connecteur vers vos machines. Si vous vous reconnaissez dans au moins 3 de ces points, il est temps d'envisager une migration.", Category = "ERP Industriel", Author = authors[1], ImageUrl = images[1], IsPublished = true, CreatedAt = DateTime.UtcNow.AddDays(-rng.Next(30, 180)) },
                    new BlogPost { Id = 3, Title = "Retour d'expérience : migration ERP en 4 mois chez un fabricant de composants automobiles", Content = "La société Mécaniques du Centre, spécialisée dans l'usinage de précision, a opté pour SimSoft ERP après 10 ans avec un logiciel legacy. Le projet a été mené en mode agile avec des sprints de 2 semaines. La migration des données historiques (5 ans de production), la formation de 45 utilisateurs et le démarrage en production ont été réalisés en 4 mois. Résultat : -30% sur les délais de traitement des commandes et +15% de productivité dès le premier trimestre.", Category = "ERP Industriel", Author = authors[2], ImageUrl = images[2], IsPublished = true, CreatedAt = DateTime.UtcNow.AddDays(-rng.Next(30, 180)) },
                    new BlogPost { Id = 4, Title = "ERP Cloud vs ERP On-Premise : lequel choisir pour votre industrie ?", Content = "Le choix entre une solution cloud et une installation sur site est stratégique. Le cloud offre une mise à jour automatique, un accès distant et un coût d'infrastructure réduit. L'on-premise garantit la souveraineté des données, la personnalisation profonde et l'indépendance réseau — critique dans les environnements OT isolés. SimSoft propose les deux modèles de déploiement, adaptés à chaque contexte réglementaire et opérationnel.", Category = "ERP Industriel", Author = authors[3], ImageUrl = images[3], IsPublished = true, CreatedAt = DateTime.UtcNow.AddDays(-rng.Next(30, 180)) },
                    new BlogPost { Id = 5, Title = "Intégration ERP-MES : le duo gagnant pour l'industrie 4.0", Content = "L'ERP gère le flux d'information de l'entreprise (commandes, stocks, finances), tandis que le MES pilote l'atelier en temps réel. Leur intégration crée une synergie puissante : les ordres de fabrication de l'ERP alimentent automatiquement le MES, et les données d'exécution remontent dans l'ERP sans ressaisie. SimSoft propose un connecteur natif certifié pour les principaux MES du marché.", Category = "ERP Industriel", Author = authors[4], ImageUrl = images[4], IsPublished = true, CreatedAt = DateTime.UtcNow.AddDays(-rng.Next(30, 180)) },
                    new BlogPost { Id = 6, Title = "Gestion des stocks intelligente avec SimSoft ERP : réduisez vos immobilisations de 20%", Content = "Le sur-stockage coûte cher. Les entreprises industrielles immobilisent en moyenne 18% de leur capital annuel dans des stocks excessifs. SimSoft ERP intègre un moteur de prévision basé sur l'historique de consommation et les tendances saisonnières. Les seuils de réapprovisionnement sont calculés dynamiquement, déclenchant des propositions d'achat automatiques soumises à validation.", Category = "ERP Industriel", Author = authors[0], ImageUrl = images[5], IsPublished = true, CreatedAt = DateTime.UtcNow.AddDays(-rng.Next(30, 180)) },
                    new BlogPost { Id = 7, Title = "Traçabilité totale des lots : l'exigence réglementaire devenue avantage concurrentiel", Content = "Dans les secteurs agroalimentaire, pharmaceutique et automobile, la traçabilité des lots n'est plus une option. SimSoft ERP permet de retrouver en moins de 30 secondes tous les composants entrant dans un produit fini et d'identifier tous les clients ayant reçu un lot suspect.", Category = "ERP Industriel", Author = authors[1], ImageUrl = images[6], IsPublished = true, CreatedAt = DateTime.UtcNow.AddDays(-rng.Next(30, 180)) },
                    new BlogPost { Id = 8, Title = "Module Finance SimSoft : clôture mensuelle en 2 jours au lieu de 10", Content = "La clôture financière est souvent un calvaire dans les industries complexes avec plusieurs centres de coûts. SimSoft ERP automatise les écritures d'inventaire, l'allocation des charges indirectes et le rapprochement bancaire. Nos clients réalisent leur clôture mensuelle en 2 jours ouvrés contre 10 en moyenne.", Category = "ERP Industriel", Author = authors[2], ImageUrl = images[0], IsPublished = false, CreatedAt = DateTime.UtcNow.AddDays(-rng.Next(1, 15)) },

                    // GMAO & Maintenance (7)
                    new BlogPost { Id = 9, Title = "La maintenance préventive conditionnelle : comment réduire vos arrêts de 40%", Content = "La maintenance curative représente en moyenne 60% des budgets de maintenance industrielle. SimSoft GMAO intègre un module de maintenance conditionnelle qui analyse les données capteurs (vibrations, température, courant moteur) pour déclencher les interventions au bon moment.", Category = "GMAO & Maintenance", Author = authors[3], ImageUrl = images[1], IsPublished = true, CreatedAt = DateTime.UtcNow.AddDays(-rng.Next(30, 180)) },
                    new BlogPost { Id = 10, Title = "Digitaliser votre bon de travail : la première étape vers une GMAO efficace", Content = "Le bon de travail papier est l'ennemi de la productivité de la maintenance. SimSoft GMAO numérise l'intégralité du cycle : création de l'ordre de travail, assignation au technicien sur mobile, relevé des pièces consommées, signature électronique et archivage automatique.", Category = "GMAO & Maintenance", Author = authors[4], ImageUrl = images[2], IsPublished = true, CreatedAt = DateTime.UtcNow.AddDays(-rng.Next(30, 180)) },
                    new BlogPost { Id = 11, Title = "Indicateurs clés pour piloter votre maintenance : TRS, MTBF, MTTR expliqués", Content = "Le Taux de Rendement Synthétique (TRS) est l'indicateur roi de la performance industrielle. Associé au MTBF et au MTTR, il donne une image complète de la santé de votre parc machines. SimSoft GMAO calcule ces KPIs automatiquement à partir des données d'intervention.", Category = "GMAO & Maintenance", Author = authors[0], ImageUrl = images[3], IsPublished = true, CreatedAt = DateTime.UtcNow.AddDays(-rng.Next(30, 180)) },
                    new BlogPost { Id = 12, Title = "Gestion du parc d'équipements : du simple inventaire à la vision patrimoniale", Content = "Chaque entreprise industrielle possède un patrimoine d'équipements souvent sous-évalué. SimSoft GMAO permet de gérer l'intégralité du cycle de vie d'un équipement : acquisition, mise en service, historique des interventions, coût total de possession (TCO), et décision de réforme.", Category = "GMAO & Maintenance", Author = authors[1], ImageUrl = images[4], IsPublished = true, CreatedAt = DateTime.UtcNow.AddDays(-rng.Next(30, 180)) },
                    new BlogPost { Id = 13, Title = "Intégration GMAO-ERP : plus de rupture de stock de pièces de rechange", Content = "Les pannes dues à l'absence de pièces de rechange représentent 15% des arrêts non planifiés. En connectant SimSoft GMAO à SimSoft ERP, les demandes de pièces créent automatiquement des demandes d'achat dans l'ERP avec suivi de livraison.", Category = "GMAO & Maintenance", Author = authors[2], ImageUrl = images[5], IsPublished = true, CreatedAt = DateTime.UtcNow.AddDays(-rng.Next(30, 180)) },
                    new BlogPost { Id = 14, Title = "Application mobile technicien : la GMAO dans la poche", Content = "Les techniciens de maintenance ne peuvent pas passer leur temps au bureau. SimSoft GMAO propose une application mobile (iOS & Android) qui fonctionne même sans connexion internet, avec synchronisation automatique.", Category = "GMAO & Maintenance", Author = authors[3], ImageUrl = images[6], IsPublished = true, CreatedAt = DateTime.UtcNow.AddDays(-rng.Next(30, 180)) },
                    new BlogPost { Id = 15, Title = "Audit GMAO : êtes-vous prêt pour la certification ISO 55001 ?", Content = "La norme ISO 55001 sur le management des actifs physiques devient une exigence contractuelle dans de nombreux secteurs. SimSoft GMAO est conçu pour répondre à ces exigences avec des rapports d'audit pré-formatés.", Category = "GMAO & Maintenance", Author = authors[4], ImageUrl = images[0], IsPublished = true, CreatedAt = DateTime.UtcNow.AddDays(-rng.Next(30, 180)) },

                    // IoT & Edge Computing (7)
                    new BlogPost { Id = 16, Title = "Edge Computing vs Cloud Computing : quel choix pour votre usine connectée ?", Content = "Dans les environnements industriels, la latence est souvent inacceptable. L'Edge Computing répond à ce besoin en traitant les données au plus proche du terrain. SimSoft Edge Gateway analyse les données des capteurs localement, ne remontant au cloud que les agrégats utiles.", Category = "IoT & Edge Computing", Author = authors[0], ImageUrl = images[1], IsPublished = true, CreatedAt = DateTime.UtcNow.AddDays(-rng.Next(30, 180)) },
                    new BlogPost { Id = 17, Title = "OPC-UA : le protocole universel qui unifie votre atelier hétérogène", Content = "Dans un atelier industriel moderne, on trouve souvent des machines de 10 marques différentes parlant 10 protocoles différents. OPC-UA s'est imposé comme le standard d'interopérabilité. SimSoft propose des adaptateurs pour Siemens, Rockwell, Schneider et Beckhoff.", Category = "IoT & Edge Computing", Author = authors[1], ImageUrl = images[2], IsPublished = true, CreatedAt = DateTime.UtcNow.AddDays(-rng.Next(30, 180)) },
                    new BlogPost { Id = 18, Title = "Jumeau numérique : de la buzzword à la réalité opérationnelle", Content = "Le concept de jumeau numérique fascine depuis plusieurs années, mais beaucoup d'entreprises peinent à en tirer de la valeur concrète. SimSoft propose une approche pragmatique : commencer par le jumeau de performance, puis évoluer vers le jumeau de simulation.", Category = "IoT & Edge Computing", Author = authors[2], ImageUrl = images[3], IsPublished = true, CreatedAt = DateTime.UtcNow.AddDays(-rng.Next(30, 180)) },
                    new BlogPost { Id = 19, Title = "5 cas d'usage IoT industriel qui ont transformé des PMI tunisiennes", Content = "La transformation numérique n'est pas réservée aux grands groupes. Voici 5 exemples concrets : monitoring de consommation énergétique (économie de 23%), traçabilité température en agroalimentaire, détection de micro-arrêts en textile, suivi de flotte de chariots élévateurs.", Category = "IoT & Edge Computing", Author = authors[3], ImageUrl = images[4], IsPublished = true, CreatedAt = DateTime.UtcNow.AddDays(-rng.Next(30, 180)) },
                    new BlogPost { Id = 20, Title = "Connecter vos machines legacy à votre SCADA en 2025 : les meilleures pratiques", Content = "Des milliers de machines industrielles fabriquées avant l'ère IoT tournent encore dans les ateliers. La rétrofitting IoT consiste à ajouter des capteurs et une passerelle de communication sans modifier leur programmation. SimSoft IoT Kit propose une solution plug-and-play.", Category = "IoT & Edge Computing", Author = authors[4], ImageUrl = images[5], IsPublished = true, CreatedAt = DateTime.UtcNow.AddDays(-rng.Next(30, 180)) },
                    new BlogPost { Id = 21, Title = "MQTT vs AMQP : choisir le bon protocole pour votre backbone IoT industriel", Content = "Le choix du protocole de messaging IoT conditionne la scalabilité et la résilience de votre infrastructure. MQTT, léger et conçu pour les réseaux instables, est idéal pour les capteurs contraints. SimSoft supporte les deux protocoles avec un broker hybride intelligent.", Category = "IoT & Edge Computing", Author = authors[0], ImageUrl = images[6], IsPublished = true, CreatedAt = DateTime.UtcNow.AddDays(-rng.Next(30, 180)) },
                    new BlogPost { Id = 22, Title = "Efficacité énergétique par l'IoT : réduisez votre facture électrique de 25%", Content = "L'énergie est souvent le 2ème poste de coût dans l'industrie. SimSoft Energy Manager identifie automatiquement les gisements d'économies : machines en veille consommant à pleine charge, pics de puissance évitables, éclairage non optimisé. Nos clients réduisent leur facture de 20 à 30%.", Category = "IoT & Edge Computing", Author = authors[1], ImageUrl = images[0], IsPublished = false, CreatedAt = DateTime.UtcNow.AddDays(-rng.Next(1, 10)) },

                    // Cybersécurité OT/IT (6)
                    new BlogPost { Id = 23, Title = "Cybersécurité industrielle : pourquoi votre OT est la cible privilégiée des hackers en 2025", Content = "Les attaques sur les systèmes industriels ont augmenté de 140% en 2024. L'OT a été conçu pour la disponibilité, pas pour la sécurité. Les automates et SCADA tournent souvent avec des OS obsolètes, non patchés, exposés sur des réseaux insuffisamment segmentés.", Category = "Cybersécurité (OT/IT)", Author = authors[2], ImageUrl = images[1], IsPublished = true, CreatedAt = DateTime.UtcNow.AddDays(-rng.Next(30, 180)) },
                    new BlogPost { Id = 24, Title = "Norme IEC 62443 : le référentiel incontournable pour sécuriser vos systèmes industriels", Content = "La norme IEC 62443 définit les exigences de sécurité pour les systèmes d'automatisation et de contrôle industriels. SimSoft a fait certifier ses produits conformes au niveau SL-2 de cette norme, garantissant une protection robuste contre les cyberattaques ciblées.", Category = "Cybersécurité (OT/IT)", Author = authors[3], ImageUrl = images[2], IsPublished = true, CreatedAt = DateTime.UtcNow.AddDays(-rng.Next(30, 180)) },
                    new BlogPost { Id = 25, Title = "Segmentation réseau IT/OT : le pilier de votre architecture de cybersécurité industrielle", Content = "La première règle de la cybersécurité industrielle est de ne jamais connecter directement votre réseau OT à Internet. La DMZ industrielle doit être soigneusement architecturée avec des pare-feux industriels et des diodes de données unidirectionnelles.", Category = "Cybersécurité (OT/IT)", Author = authors[4], ImageUrl = images[3], IsPublished = true, CreatedAt = DateTime.UtcNow.AddDays(-rng.Next(30, 180)) },
                    new BlogPost { Id = 26, Title = "Détection d'intrusion industrielle : identifier une attaque avant qu'il ne soit trop tard", Content = "Un attaquant professionnel reste en moyenne 200 jours dans un réseau avant d'être détecté. SimSoft IDS Industriel analyse les communications Modbus, Profinet et OPC-UA pour détecter les comportements anormaux.", Category = "Cybersécurité (OT/IT)", Author = authors[0], ImageUrl = images[4], IsPublished = true, CreatedAt = DateTime.UtcNow.AddDays(-rng.Next(30, 180)) },
                    new BlogPost { Id = 27, Title = "Gestion des accès distants sécurisés pour vos techniciens et fournisseurs", Content = "SimSoft Remote Access Manager propose une solution de VPN industriel avec authentification multi-facteurs, enregistrement des sessions, et accès granulaire limité dans le temps. L'accès d'un fournisseur est accordé pour 2 heures et enregistré intégralement.", Category = "Cybersécurité (OT/IT)", Author = authors[1], ImageUrl = images[5], IsPublished = true, CreatedAt = DateTime.UtcNow.AddDays(-rng.Next(30, 180)) },
                    new BlogPost { Id = 28, Title = "Plan de réponse aux incidents industriels : êtes-vous prêts pour le jour J ?", Content = "La question n'est pas 'si' votre système sera attaqué, mais 'quand'. Avoir un plan de réponse aux incidents (IRP) est critique. SimSoft propose des ateliers de simulation de crise (cyber-exercices) pour tester votre plan avant d'en avoir besoin.", Category = "Cybersécurité (OT/IT)", Author = authors[2], ImageUrl = images[6], IsPublished = false, CreatedAt = DateTime.UtcNow.AddDays(-rng.Next(1, 7)) },

                    // Transformation Digitale (6)
                    new BlogPost { Id = 29, Title = "Feuille de route pour une transformation digitale réussie dans l'industrie manufacturière", Content = "La transformation digitale n'est pas un projet informatique, c'est un projet d'entreprise. Elle commence par la définition d'une vision claire et passe par des quick wins visibles. SimSoft accompagne ses clients avec une méthodologie éprouvée sur 200+ projets.", Category = "Transformation Digitale", Author = authors[3], ImageUrl = images[0], IsPublished = true, CreatedAt = DateTime.UtcNow.AddDays(-rng.Next(30, 180)) },
                    new BlogPost { Id = 30, Title = "Change management industriel : comment embarquer vos opérateurs dans la digitalisation", Content = "Le plus grand risque de la transformation digitale n'est pas technologique, c'est humain. SimSoft intègre une démarche de change management dans tous ses projets : ambassadeurs terrain, formation adaptée et suivi de l'adoption dans les 6 premiers mois.", Category = "Transformation Digitale", Author = authors[4], ImageUrl = images[1], IsPublished = true, CreatedAt = DateTime.UtcNow.AddDays(-rng.Next(30, 180)) },
                    new BlogPost { Id = 31, Title = "Intelligence Artificielle en industrie : mythe ou réalité applicable dès aujourd'hui ?", Content = "L'IA est partout dans les discours. La réalité applicable : vision par ordinateur pour le contrôle qualité, maintenance prédictive sur données capteurs, et optimisation des plans de production. SimSoft AI intègre ces trois cas d'usage sans data scientist interne.", Category = "Transformation Digitale", Author = authors[0], ImageUrl = images[2], IsPublished = true, CreatedAt = DateTime.UtcNow.AddDays(-rng.Next(30, 180)) },
                    new BlogPost { Id = 32, Title = "La paperasse zéro : comment digitaliser vos procédures qualité et sécurité", Content = "SimSoft Forms permet de numériser n'importe quel formulaire papier en quelques heures, avec signature électronique, photos jointes, et archivage automatique conforme aux normes ISO 9001 et ISO 45001.", Category = "Transformation Digitale", Author = authors[1], ImageUrl = images[3], IsPublished = true, CreatedAt = DateTime.UtcNow.AddDays(-rng.Next(30, 180)) },
                    new BlogPost { Id = 33, Title = "Industrie 4.0 en Tunisie : état des lieux et perspectives 2025-2030", Content = "La Tunisie industrielle est à un tournant. Les donneurs d'ordre européens exigent des preuves de digitalisation. Les industriels tunisiens qui n'investissent pas maintenant risquent de perdre des marchés dans 3 à 5 ans.", Category = "Transformation Digitale", Author = authors[2], ImageUrl = images[4], IsPublished = true, CreatedAt = DateTime.UtcNow.AddDays(-rng.Next(30, 180)) },
                    new BlogPost { Id = 34, Title = "ROI de la digitalisation industrielle : comment calculer et présenter votre business case", Content = "Convaincre sa direction d'investir dans la digitalisation nécessite un business case solide. Les bénéfices : réduction des rebuts (2-4% du CA), économies de main-d'œuvre, réduction des stocks. SimSoft propose un calculateur de ROI personnalisé.", Category = "Transformation Digitale", Author = authors[3], ImageUrl = images[5], IsPublished = true, CreatedAt = DateTime.UtcNow.AddDays(-rng.Next(30, 180)) },

                    // Caisse & Encaissement (4)
                    new BlogPost { Id = 35, Title = "Système de caisse moderne : les fonctionnalités indispensables en 2025", Content = "Un système de caisse ne se limite plus à l'encaissement. Il doit gérer la fidélité client, les paiements multiples, les promotions complexes, et la synchronisation temps réel avec le stock. SimSoft Caisse intègre toutes ces fonctionnalités dans une interface tactile.", Category = "Caisse & Encaissement", Author = authors[4], ImageUrl = images[6], IsPublished = true, CreatedAt = DateTime.UtcNow.AddDays(-rng.Next(30, 180)) },
                    new BlogPost { Id = 36, Title = "Conformité fiscale et dématérialisation des factures : ce que la loi exige de vous", Content = "La réglementation fiscale évolue rapidement vers la dématérialisation obligatoire des factures. SimSoft Caisse génère automatiquement des factures électroniques conformes avec signature numérique et archivage légal pendant 10 ans.", Category = "Caisse & Encaissement", Author = authors[0], ImageUrl = images[0], IsPublished = true, CreatedAt = DateTime.UtcNow.AddDays(-rng.Next(30, 180)) },
                    new BlogPost { Id = 37, Title = "Gestion multi-sites : piloter 20 points de vente depuis un seul tableau de bord", Content = "SimSoft Retail Central permet de paramétrer centralement les catalogues, les prix et les promotions, et de consolider les ventes en temps réel depuis le siège. Vision instantanée des performances par point de vente, par zone ou par vendeur.", Category = "Caisse & Encaissement", Author = authors[1], ImageUrl = images[1], IsPublished = true, CreatedAt = DateTime.UtcNow.AddDays(-rng.Next(30, 180)) },
                    new BlogPost { Id = 38, Title = "Mode déconnecté : votre caisse continue de fonctionner même sans internet", Content = "SimSoft Caisse est conçu pour fonctionner intégralement en mode hors ligne. Les transactions sont stockées localement et synchronisées dès le retour de la connexion, garantissant une disponibilité de service même dans les zones à faible connectivité.", Category = "Caisse & Encaissement", Author = authors[2], ImageUrl = images[2], IsPublished = true, CreatedAt = DateTime.UtcNow.AddDays(-rng.Next(30, 180)) },

                    // Général (2)
                    new BlogPost { Id = 39, Title = "SimSoft Technologies fête ses 25 ans : une success story tunisienne du logiciel industriel", Content = "Fondée en 2000 à Tunis, SimSoft Technologies célèbre cette année ses 25 ans. De la première solution de gestion de production à la plateforme IoT industrielle couvrant aujourd'hui plus de 500 clients dans 8 pays, c'est avant tout l'histoire d'une équipe soudée.", Category = "Général", Author = authors[3], ImageUrl = images[3], IsPublished = true, CreatedAt = DateTime.UtcNow.AddDays(-rng.Next(1, 30)) },
                    new BlogPost { Id = 40, Title = "Rejoignez l'équipe SimSoft : nous recrutons des développeurs, consultants et ingénieurs IoT", Content = "SimSoft Technologies est en forte croissance et recrute des talents passionnés. Nous cherchons des développeurs .NET et Angular, des consultants ERP, des ingénieurs IoT, et des chefs de projet en transformation digitale. Envoyez votre candidature à careers@simsoft.tn.", Category = "Général", Author = authors[4], ImageUrl = images[4], IsPublished = true, CreatedAt = DateTime.UtcNow.AddDays(-rng.Next(1, 30)) }
                };

                db.BlogPosts.AddRange(posts);
                db.SaveChanges();
            }

            // --- VISITOR REQUESTS ---
            if (!db.VisitorRequests.Any())
            {
                var rng = new Random(42);
                var statuses = new[] { "Nouveau", "Nouveau", "Nouveau", "En cours", "En cours", "Résolu" };
                var firstNames = new[] { "Mohamed", "Ahmed", "Fatma", "Sonia", "Karim", "Nabil", "Ines", "Rami", "Hajer", "Bilel", "Wafa", "Tarek", "Amira", "Slim", "Henda", "Zied", "Mariem", "Firas", "Olfa", "Aziz", "Jean-Pierre", "Marie", "François", "Sophie", "Michel", "Claire", "Laurent", "Thomas" };
                var lastNames = new[] { "Ben Ali", "Chabbi", "Mzali", "Dridi", "Hamdi", "Jouini", "Gafsi", "Khelil", "Lassoued", "Mabrouk", "Nasr", "Ouali", "Riahi", "Saidi", "Tlili", "Mejri", "Ferchichi", "Bousselmi", "Zarrouk", "Haddad", "Dupont", "Martin", "Bernard", "Petit", "Durand", "Leroy", "Moreau", "Michel" };
                var companies = new[] { "STIP", "Tunisie Câbles", "Sotuver", "Electrostar", "SOCOMENA", "Leoni Tunisie", "Coficab", "Industries Chimiques du Fluor", "STS", "Misfat", "GCT", "CPG Gafsa", "Carthago Yachts", "Poulina Group", "SAH Lilas", "Délice", "OCP Maroc", "Maghreb Steel", "Valeo Tunisie", "Yazaki Tunisie", "Aptiv", "Renault Maroc" };
                var subjects = new[] {
                    "Demande de démo SimSoft ERP", "Demande de devis GMAO", "Question sur l'intégration IoT", "Demande de prix solution de caisse multi-sites",
                    "Renseignements sur la cybersécurité OT", "Demande de contact commercial", "Intérêt pour SimSoft GMAO mobile", "Demande de démo plateforme IoT",
                    "Question technique sur OPC-UA", "Demande de formation SimSoft ERP", "Projet de digitalisation atelier", "Demande de partenariat intégrateur",
                    "Évaluation ERP pour PMI 50 personnes", "Migration depuis SAP Business One", "Connectivité machines legacy", "Demande d'audit cybersécurité",
                    "Mise en place traçabilité ISO", "Projet jumeau numérique", "Optimisation énergétique usine", "Gestion parc 200 machines"
                };
                var messages = new[] {
                    "Bonjour, nous sommes une entreprise industrielle de 120 personnes et nous cherchons à moderniser notre système de gestion de production. Pourriez-vous nous présenter votre solution ERP ?",
                    "Notre responsable maintenance souhaite déployer une GMAO pour nos 350 équipements. Nous avons besoin d'une démonstration et d'un devis personnalisé.",
                    "Nous avons actuellement 45 machines connectées via Modbus et souhaitons les intégrer à un système de supervision centralisé. Quelles sont vos solutions IoT ?",
                    "Nous gérons 8 points de vente en Tunisie et cherchons un système de caisse centralisé avec gestion des stocks en temps réel.",
                    "Suite à un audit de sécurité, nous devons segmenter nos réseaux IT et OT. Avez-vous des compétences en architecture cybersécurité industrielle ?",
                    "Je suis directeur industriel dans un fabricant de câbles. Nous cherchons à déployer une traçabilité des bobines du fournisseur au client.",
                    "Notre atelier textile a 80 machines. Nous souhaitons monitorer la production en temps réel pour réduire les arrêts non planifiés.",
                    "Nous avons un projet de digitalisation complet : ERP, GMAO, IoT et tableaux de bord. Pouvez-vous nous accompagner de A à Z ?",
                    "Quels sont vos tarifs pour une solution GMAO pour 5 techniciens et un parc de 200 équipements ?",
                    "Notre ERP actuel ne supporte pas la gestion des sous-traitants. Votre solution SimSoft ERP couvre-t-elle ce besoin ?",
                    "Nous cherchons à certifier notre système de management de la maintenance selon ISO 55001. Votre GMAO peut-elle nous accompagner ?",
                    "Nous sommes intégrateur système et souhaitons devenir partenaire revendeur de vos solutions. Quel est votre programme partenaire ?",
                    "Notre direction souhaite un tableau de bord temps réel consolidant production, qualité et énergie sur 3 sites. Est-ce possible ?",
                    "Nous avons subi une cyberattaque l'année dernière et souhaitons renforcer notre sécurité OT. Proposez-vous des audits ?",
                    "Je cherche une solution de contrôle qualité par vision artificielle pour notre ligne d'assemblage. Avez-vous des références dans ce domaine ?"
                };

                var requests = new List<VisitorRequest>();
                int id = 1;

                // Distribute ~120 requests over 30 days with realistic daily variation
                var dailyCounts = new int[] { 3,5,2,4,6,3,1,4,5,2,3,6,4,2,5,3,4,2,5,6,3,4,2,3,5,4,3,6,4,3 };

                foreach (var (count, dayIndex) in dailyCounts.Select((c, i) => (c, i)))
                {
                    var baseDate = DateTime.UtcNow.Date.AddDays(-(29 - dayIndex));
                    for (int j = 0; j < count; j++)
                    {
                        var firstName = firstNames[rng.Next(firstNames.Length)];
                        var lastName = lastNames[rng.Next(lastNames.Length)];
                        var company = companies[rng.Next(companies.Length)];
                        var domain = rng.Next(3) == 0 ? "gmail.com" : (rng.Next(2) == 0 ? "yahoo.fr" : "outlook.com");

                        requests.Add(new VisitorRequest
                        {
                            Id = id++,
                            VisitorName = $"{firstName} {lastName}",
                            VisitorEmail = $"{firstName.ToLower()}.{lastName.ToLower().Replace(" ", "").Replace("-", "")}@{domain}",
                            Subject = subjects[rng.Next(subjects.Length)],
                            Message = $"[{company}] " + messages[rng.Next(messages.Length)],
                            Status = statuses[rng.Next(statuses.Length)],
                            CreatedAt = baseDate.AddHours(rng.Next(7, 19)).AddMinutes(rng.Next(0, 59))
                        });
                    }
                }

                db.VisitorRequests.AddRange(requests);
                db.SaveChanges();
            }
        }
    }
}
