﻿/*
 * Copyright Lamont Granquist (lamont@scriptkiddie.org)
 * Dual licensed under the MIT (MIT-LICENSE) license
 * and GPLv2 (GPLv2-LICENSE) license or any later version.
 */

using System;
using AssertExtensions;
using Xunit;
using MechJebLib.Maths;
using MechJebLib.Primitives;
using static MechJebLib.Utils.Statics;

namespace MechJebLibTest.Maths
{
    public class FunctionsTests
    {
        private const double PERIOD = 86164.0905;

        private const double ACC  = EPS * 8;
        private const double ACC2 = 1e-7; // due west launches have some mathematical irregularities

        [Fact]
        void TestThis()
        {
            double mu = 3.986004418e+14;
            double peR =  6.371e+6 + 350.8e+3;
            (Math.Cos(Deg2Rad(13.71666)) / Math.Cos(Deg2Rad(28.608)) * 407.86).ShouldEqual(0.0, 1e-7);
            Functions.CircularVelocity(mu, peR).ShouldEqual(0.0, 1e-7);
        }
        
        [Fact]
        void Test90()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    double lng = i * 45;
                    double lan = j * 45;

                    // zero degree advance
                    double delay = PERIOD / 8 * ((j - i + 8) % 8);

                    if (delay != 0)
                    {
                        Functions.TimeToPlane(PERIOD,28.608,lng,lan,90).ShouldEqual(delay,ACC);
                        Functions.TimeToPlane(PERIOD,-28.608,lng,lan,90).ShouldEqual(delay,ACC);
                    }

                    // reverse
                    delay = PERIOD / 8 * ((i - j + 8) % 8);

                    if (delay != 0)
                    {
                        Functions.TimeToPlane(-PERIOD,28.608,lng,lan,90).ShouldEqual(delay,ACC);
                        Functions.TimeToPlane(-PERIOD,-28.608,lng,lan,90).ShouldEqual(delay,ACC);
                    }

                    // advance by 180 degrees
                    delay = PERIOD / 8 * ((j - i + 8 + 4) % 8);

                    if (delay != 0)
                    {
                        Functions.TimeToPlane(PERIOD,28.608,lng,lan,-90).ShouldEqual(delay,ACC);
                        Functions.TimeToPlane(PERIOD,-28.608,lng,lan,-90).ShouldEqual(delay,ACC);
                    }

                    // reverse and advance by 180 degrees
                    delay = PERIOD / 8 * ((i - j + 8 + 4) % 8);

                    if (delay != 0)
                    {
                        Functions.TimeToPlane(-PERIOD,28.608,lng,lan,-90).ShouldEqual(delay,ACC);
                        Functions.TimeToPlane(-PERIOD,-28.608,lng,lan,-90).ShouldEqual(delay,ACC);
                    }
                }
            }
        }

        [Fact]
        private void Test45At45()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    double lng = i * 45;
                    double lan = j * 45;

                    // advance by 90 degrees
                    double delay = PERIOD / 8 * ((j - i + 8 + 2) % 8);

                    if (delay != 0)
                    {
                        Functions.TimeToPlane(PERIOD,45,lng,lan,45).ShouldEqual(delay,ACC);
                        Functions.TimeToPlane(PERIOD,45,lng,lan,-45).ShouldEqual(delay,ACC);
                    }

                    // advance by 270 degrees
                    delay = PERIOD / 8 * ((j - i + 8 + 6) % 8);

                    if (delay != 0)
                    {
                        Functions.TimeToPlane(PERIOD,-45,lng,lan,45).ShouldEqual(delay,ACC);
                        Functions.TimeToPlane(PERIOD,-45,lng,lan,-45).ShouldEqual(delay,ACC);
                    }

                    // reverse and advance by 270 degrees
                    delay = PERIOD / 8 * ((i - j + 8 + 6) % 8);

                    if (delay != 0)
                    {
                        Functions.TimeToPlane(-PERIOD,45,lng,lan,45).ShouldEqual(delay,ACC);
                        Functions.TimeToPlane(-PERIOD,45,lng,lan,-45).ShouldEqual(delay,ACC);
                    }

                    // reverse and advance by 90 degrees
                    delay = PERIOD / 8 * ((i - j + 8 + 2) % 8);

                    if (delay != 0)
                    {
                        Functions.TimeToPlane(-PERIOD,-45,lng,lan,45).ShouldEqual(delay,ACC);
                        Functions.TimeToPlane(-PERIOD,-45,lng,lan,-45).ShouldEqual(delay,ACC);
                    }
                }
            }
        }

        [Fact]
        private void Test135At45()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    double lng = i * 45;
                    double lan = j * 45;

                    // advance by 270 degrees
                    double delay = PERIOD / 8 * ((j - i + 8 + 6) % 8);

                    if (delay != 0)
                    {
                        Functions.TimeToPlane(PERIOD,45,lng,lan,135).ShouldEqual(delay,ACC2);
                        Functions.TimeToPlane(PERIOD,45,lng,lan,-135).ShouldEqual(delay,ACC2);
                    }

                    // advance by 90 degrees
                    delay = PERIOD / 8 * ((j - i + 8 + 2) % 8);

                    if (delay != 0)
                    {
                        Functions.TimeToPlane(PERIOD,-45,lng,lan,135).ShouldEqual(delay,ACC2);
                        Functions.TimeToPlane(PERIOD,-45,lng,lan,-135).ShouldEqual(delay,ACC2);
                    }

                    // reverse and advance by 90 degrees
                    delay = PERIOD / 8 * ((i - j + 8 + 2) % 8);

                    if (delay != 0)
                    {
                        Functions.TimeToPlane(-PERIOD,45,lng,lan,135).ShouldEqual(delay,ACC2);
                        Functions.TimeToPlane(-PERIOD,45,lng,lan,-135).ShouldEqual(delay,ACC2);
                    }

                    // reverse and advance by 270 degrees
                    delay = PERIOD / 8 * ((i - j + 8 + 6) % 8);

                    if (delay != 0)
                    {
                        Functions.TimeToPlane(-PERIOD,-45,lng,lan,135).ShouldEqual(delay,ACC2);
                        Functions.TimeToPlane(-PERIOD,-45,lng,lan,-135).ShouldEqual(delay,ACC2);
                    }
                }
            }
        }

        [Fact]
        private void EquatorialAt45()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    double lng = i * 45;
                    double lan = j * 45;

                    // advance by 90 degrees
                    double delay = PERIOD / 8 * ((j - i + 8 + 2) % 8);

                    if (delay != 0)
                    {
                        Functions.TimeToPlane(PERIOD,45,lng,lan,0).ShouldEqual(delay,ACC);
                    }

                    // advance by 270 degrees
                    delay = PERIOD / 8 * ((j - i + 8 + 6) % 8);

                    if (delay != 0)
                    {
                        Functions.TimeToPlane(PERIOD,-45,lng,lan,0).ShouldEqual(delay,ACC);
                    }

                    // reverse and advance by 270 degrees
                    delay = PERIOD / 8 * ((i - j + 8 + 6) % 8);

                    if (delay != 0)
                    {
                        Functions.TimeToPlane(-PERIOD,45,lng,lan,0).ShouldEqual(delay,ACC);
                    }

                    // reverse and advance by 90 degrees
                    delay = PERIOD / 8 * ((i - j + 8 + 2) % 8);

                    if (delay != 0)
                    {
                        Functions.TimeToPlane(-PERIOD,-45,lng,lan,0).ShouldEqual(delay,ACC);
                    }
                }
            }
        }

        [Fact]
        private void RetrogradeEquatorialAt45()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    double lng = i * 45;
                    double lan = j * 45;

                    // advance by 270 degrees
                    double delay = PERIOD / 8 * ((j - i + 8 + 6) % 8);

                    if (delay != 0)
                    {
                        Functions.TimeToPlane(PERIOD,45,lng,lan,180).ShouldEqual(delay,ACC2);
                        Functions.TimeToPlane(PERIOD,45,lng,lan,-180).ShouldEqual(delay,ACC2);
                    }

                    // advance by 90 degrees
                    delay = PERIOD / 8 * ((j - i + 8 + 2) % 8);

                    if (delay != 0)
                    {
                        Functions.TimeToPlane(PERIOD,-45,lng,lan,180).ShouldEqual(delay,ACC2);
                        Functions.TimeToPlane(PERIOD,-45,lng,lan,-180).ShouldEqual(delay,ACC2);
                    }

                    // reverse and advance by 90 degrees
                    delay = PERIOD / 8 * ((i - j + 8 + 2) % 8);

                    if (delay != 0)
                    {
                        Functions.TimeToPlane(-PERIOD,45,lng,lan,180).ShouldEqual(delay,ACC2);
                        Functions.TimeToPlane(-PERIOD,45,lng,lan,-180).ShouldEqual(delay,ACC2);
                    }

                    // reverse and advance by 270 degrees
                    delay = PERIOD / 8 * ((i - j + 8 + 6) % 8);

                    if (delay != 0)
                    {
                        Functions.TimeToPlane(-PERIOD,-45,lng,lan,180).ShouldEqual(delay,ACC2);
                        Functions.TimeToPlane(-PERIOD,-45,lng,lan,-180).ShouldEqual(delay,ACC2);
                    }
                }
            }
        }

        [Fact]
        private void Test47AtKSCLat()
        {
            // this produces a 330 degree LAN from 28.608 so everything is 30 degrees offset
            const double inc = 47.486638356389;

            for (int i = 0; i < 12; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    double lng = i * 30;
                    double lan = j * 30;

                    // advance by 30 degrees
                    double delay = PERIOD / 12 * ((j - i + 12 + 1) % 12);

                    if (delay != 0)
                    {
                        Functions.TimeToPlane(PERIOD,28.608,lng,lan,inc).ShouldEqual(delay,ACC);
                    }

                    // advance by 150 degrees
                    delay = PERIOD / 12 * ((j - i + 12 + 5) % 12);

                    if (delay != 0)
                    {
                        Functions.TimeToPlane(PERIOD,28.608,lng,lan,-inc).ShouldEqual(delay,ACC);
                    }

                    // advance by 330 degrees
                    delay = PERIOD / 12 * ((j - i + 12 + 11) % 12);

                    if (delay != 0)
                    {
                        Functions.TimeToPlane(PERIOD,-28.608,lng,lan,inc).ShouldEqual(delay,ACC);
                    }

                    // advance by 210 degrees
                    delay = PERIOD / 12 * ((j - i + 12 + 7) % 12);

                    if (delay != 0)
                    {
                        Functions.TimeToPlane(PERIOD,-28.608,lng,lan,-inc).ShouldEqual(delay,ACC);
                    }

                    // reverse and advance by 330 degrees
                    delay = PERIOD / 12 * ((i - j + 12 + 11) % 12);

                    if (delay != 0)
                    {
                        Functions.TimeToPlane(-PERIOD,28.608,lng,lan,inc).ShouldEqual(delay,ACC);
                    }

                    // reverse and advance by 210 degrees
                    delay = PERIOD / 12 * ((i - j + 12 + 7) % 12);

                    if (delay != 0)
                    {
                        Functions.TimeToPlane(-PERIOD,28.608,lng,lan,-inc).ShouldEqual(delay,ACC);
                    }

                    // advance by 30 degrees
                    delay = PERIOD / 12 * ((i - j + 12 + 1) % 12);

                    if (delay != 0)
                    {
                        Functions.TimeToPlane(-PERIOD,-28.608,lng,lan,inc).ShouldEqual(delay,ACC);
                    }

                    // advance by 150 degrees
                    delay = PERIOD / 12 * ((i - j + 12 + 5) % 12);

                    if (delay != 0)
                    {
                        Functions.TimeToPlane(-PERIOD,-28.608,lng,lan,-inc).ShouldEqual(delay,ACC);
                    }
                }
            }
        }

        [Fact]
        private void Test132AtKSCLat()
        {
            // similar to the 47 degree tests only retrograde
            const double inc = 180 - 47.486638356389;

            for (int i = 0; i < 12; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    double lng = i * 30;
                    double lan = j * 30;

                    // advance by 330 degrees
                    double delay = PERIOD / 12 * ((j - i + 12 + 11) % 12);

                    if (delay != 0)
                    {
                        Functions.TimeToPlane(PERIOD,28.608,lng,lan,inc).ShouldEqual(delay,ACC);
                    }

                    // advance by 210 degrees
                    delay = PERIOD / 12 * ((j - i + 12 + 7) % 12);

                    if (delay != 0)
                    {
                        Functions.TimeToPlane(PERIOD,28.608,lng,lan,-inc).ShouldEqual(delay,ACC);
                    }

                    // advance by 30 degrees
                    delay = PERIOD / 12 * ((j - i + 12 + 1) % 12);

                    if (delay != 0)
                    {
                        Functions.TimeToPlane(PERIOD,-28.608,lng,lan,inc).ShouldEqual(delay,ACC);
                    }

                    // advance by 150 degrees
                    delay = PERIOD / 12 * ((j - i + 12 + 5) % 12);

                    if (delay != 0)
                    {
                        Functions.TimeToPlane(PERIOD,-28.608,lng,lan,-inc).ShouldEqual(delay,ACC);
                    }

                    // reverse and advance by 30 degrees
                    delay = PERIOD / 12 * ((i-j + 12 + 1) % 12);

                    if (delay != 0)
                    {
                        Functions.TimeToPlane(-PERIOD,28.608,lng,lan,inc).ShouldEqual(delay,ACC);
                    }

                    // reverse and advance by 150 degrees
                    delay = PERIOD / 12 * ((i-j + 12 + 5) % 12);

                    if (delay != 0)
                    {
                        Functions.TimeToPlane(-PERIOD,28.608,lng,lan,-inc).ShouldEqual(delay,ACC);
                    }

                    // reverse and advance by 330 degrees
                    delay = PERIOD / 12 * ((i -j + 12 + 11) % 12);

                    if (delay != 0)
                    {
                        Functions.TimeToPlane(-PERIOD,-28.608,lng,lan,inc).ShouldEqual(delay,ACC);
                    }

                    // reverse and advance by 210 degrees
                    delay = PERIOD / 12 * ((i-j + 12 + 7) % 12);

                    if (delay != 0)
                    {
                        Functions.TimeToPlane(-PERIOD,-28.608,lng,lan,-inc).ShouldEqual(delay,ACC);
                    }
                }
            }
        }

        [Fact]
        private void Poles()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    for (int k = 0; k <= 8; k++)
                    {
                        double lng = i * 45;
                        double lan = j * 45;
                        double inc = k * 45 - 180;
                        Functions.TimeToPlane(PERIOD,90,lng,lan,inc).ShouldBeZero(ACC);
                        Functions.TimeToPlane(PERIOD,-90,lng,lan,inc).ShouldBeZero(ACC);
                    }
                }
            }
        }

        [Fact]
        public void ECIToOrbTest1()
        {
            double mu = 3.986004418000000e+14;
            double smaEx = 3.843084377707066e+08;
            double eccEx = 5.328149353682574e-02;
            double incEx = 4.950221141769940e-01;
            double argpEx = 3.486541150390846e+00;
            double lanEx = 4.008351366616158e-02;
            double tanomEx = 7.853981633974483e-01;

            var r = new V3(
                -1.455451021873417e+08,
                -3.000298697925529e+08,
                -1.586943000620733e+08
            );
            var v = new V3(
                9.572921091669031e+02,
                -3.895747803416348e+02,
                -2.308551508912105e+02
            );

            (double sma, double ecc, double inc, double lan, double argp, double tanom) = Functions.KeplerianFromStateVectors(mu, r, v);
            sma.ShouldEqual(smaEx, 1e-15);
            ecc.ShouldEqual(eccEx, 1e-14);
            inc.ShouldEqual(incEx, 1e-15);
            lan.ShouldEqual(lanEx, 1e-15);
            argp.ShouldEqual(argpEx, 1e-15);
            tanom.ShouldEqual(tanomEx, 1e-14);
        }
    }
}
