// Decompiled with JetBrains decompiler
// Type: OpenTracksBETA.ViewModels.MainViewModel
// Assembly: OpenTracksBETA, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 37D9428F-DECE-49E6-8BF2-BFFD8E04DA19
// Assembly location: C:\Users\Admin\Desktop\RE\OpenTracksCE\OpenTracksBETA.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Windows.Media.Playback; // Added for MediaPlayer
using Windows.Media.Core; // Added for MediaSource

#nullable disable

namespace OpenTracksBETA.ViewModels
{
  public class MainViewModel : INotifyPropertyChanged
  {
    private ObservableCollection<TrackItem> _allTracks;
    private ObservableCollection<TrackItem> _topTracks;
    private ObservableCollection<TrackItem> _filteredTracks;
    private TrackItem _currentTrack;

    public ICommand PlayCommand { get; private set; }

    public ObservableCollection<TrackItem> AllTracks
    {
      get => this._allTracks;
      set
      {
        if (this._allTracks == value)
          return;
        this._allTracks = value;
        this.OnPropertyChanged(nameof (AllTracks));
      }
    }

    public ObservableCollection<TrackItem> TopTracks
    {
      get => this._topTracks;
      set
      {
        if (this._topTracks == value)
          return;
        this._topTracks = value;
        this.OnPropertyChanged(nameof (TopTracks));
      }
    }

    public ObservableCollection<TrackItem> FilteredTracks
    {
      get => this._filteredTracks;
      set
      {
        this._filteredTracks = value;
        this.OnPropertyChanged(nameof (FilteredTracks));
      }
    }

    public TrackItem CurrentTrack
    {
      get => this._currentTrack;
      set
      {
        if (this._currentTrack == value)
          return;
        this._currentTrack = value;
        this.OnPropertyChanged(nameof (CurrentTrack));
      }
    }

    public MainViewModel()
    {
      this.PlayCommand = (ICommand) new RelayCommand<TrackItem>(new Action<TrackItem>(this.PlayTrack));
      ObservableCollection<TrackItem> observableCollection1 = new ObservableCollection<TrackItem>();
      observableCollection1.Add(new TrackItem()
      {
        Title = "STARVING RAVEN",
        AudioPath = "https://ia800906.us.archive.org/29/items/penseesdeauville/%E3%80%90%E6%9D%B1%E6%96%B9Vocal%EF%BC%8FEurobeat%E3%80%91%20STARVING%20RAVEN%20%E3%80%8CSOUND%20HOLIC%E3%80%8D.mp3",
        AlbumCoverPath = "https://open-tracks.vercel.app/Assets/ncs/2.jpg",
        Artist = "nana takahashi, jp remixi"
      });
      observableCollection1.Add(new TrackItem()
      {
        Title = "NIGHTS OF NIGHTS",
        AudioPath = "https://ia601500.us.archive.org/12/items/all-night-of-knights-cool-and-create/01.%20Night%20of%20Knights.mp3",
        AlbumCoverPath = "https://dl.marmak.net.pl/images/009%20Sound%20System/009%20Sound%20System.jpg",
        Artist = "Flowering Night Remix"
      });
      observableCollection1.Add(new TrackItem()
      {
        Title = "IGNITE THE POWER",
        AudioPath = "https://ia800906.us.archive.org/29/items/penseesdeauville/IGNITE%20THE%20POWER.mp3",
        AlbumCoverPath = "https://i1.sndcdn.com/artworks-000587244338-vn5orc-t500x500.jpg",
        Artist = "nana takahashi, th remix"
      });
      observableCollection1.Add(new TrackItem()
      {
        Title = "00 HEAVEN",
        AudioPath = "https://ia800906.us.archive.org/29/items/penseesdeauville/%E3%80%90%E6%9D%B1%E6%96%B9Vocal%EF%BC%8FEurobeat%E3%80%91%2000%20HEAVEN%20%E3%80%8CSOUND%20HOLIC%E3%80%8D.mp3",
        AlbumCoverPath = "https://i1.sndcdn.com/artworks-000567230594-q8qfvo-t1080x1080.jpg",
        Artist = "nana takahashi, th remix"
      });
      observableCollection1.Add(new TrackItem()
      {
        Title = "FIRST STORM",
        AudioPath = "https://ia800906.us.archive.org/29/items/penseesdeauville/FIRST%20STORM%20%EF%BD%9C%20%F0%9D%90%BB%F0%9D%91%8E%F0%9D%91%A1%F0%9D%91%A0%F0%9D%91%A2%F0%9D%91%9B%F0%9D%91%92%20%F0%9D%91%80%F0%9D%91%96%F0%9D%91%98%F0%9D%91%A2%20%EF%BD%9C%20No%20Copyright%20%F0%9F%8E%B5.mp3",
        AlbumCoverPath = "https://dl.marmak.net.pl/images/009%20Sound%20System/009%20Sound%20System.jpg",
        Artist = "Hatsune Miku"
      });
      observableCollection1.Add(new TrackItem()
      {
        Title = "HAZY MOON",
        AudioPath = "https://ia600906.us.archive.org/29/items/penseesdeauville/Hazy%20Moon%20-%20Hatsune%20Miku%20%28%20no%20copyright%20music%20%29.mp3",
        AlbumCoverPath = "https://i1.sndcdn.com/artworks-000587244338-vn5orc-t500x500.jpg",
        Artist = "Hatsune Miku"
      });
      observableCollection1.Add(new TrackItem()
      {
        Title = "Ievan Polkka (Sped up)",
        AudioPath = "https://ia800906.us.archive.org/29/items/penseesdeauville/Ievan%20Polkka%20Song%20-%20Hatsune%20Miku%20%28Copyright%20Free%20Song%29.mp3",
        AlbumCoverPath = "https://i1.sndcdn.com/artworks-000567230594-q8qfvo-t1080x1080.jpg",
        Artist = "Hatsune Miku"
      });
      observableCollection1.Add(new TrackItem()
      {
        Title = "DIVE INTO STREAM",
        AudioPath = "https://ia600906.us.archive.org/29/items/penseesdeauville/dintos.mp3",
        AlbumCoverPath = "https://open-tracks.vercel.app/Assets/ncs/2.jpg",
        Artist = "Hatsune Miku"
      });
      observableCollection1.Add(new TrackItem()
      {
        Title = "Nekozilla",
        AudioPath = "https://ia803109.us.archive.org/27/items/generic-nekozilla-lfz-remix-_different-heaven_-6189037-wEqcGyYvm/nekozilla-lfz-remix-_different-heaven_-6189037-wEqcGyYvm-nekozilla-lfz-remix-_different-heaven_-6189037-wEqcGyYvm.mp3",
        Artist = "LFZ Remix"
      });
      observableCollection1.Add(new TrackItem()
      {
        Title = "Windows",
        AudioPath = "https://ia801701.us.archive.org/11/items/stilliloveyou/K-391%20-%20Windows.mp3",
        Artist = "K391"
      });
      this.TopTracks = observableCollection1;
      ObservableCollection<TrackItem> observableCollection2 = new ObservableCollection<TrackItem>();
      observableCollection2.Add(new TrackItem()
      {
        Title = "Windows",
        AudioPath = "https://ia801701.us.archive.org/11/items/stilliloveyou/K-391%20-%20Windows.mp3",
        Artist = "K391"
      });
      observableCollection2.Add(new TrackItem()
      {
        Title = "STARVING RAVEN",
        AudioPath = "https://ia800906.us.archive.org/29/items/penseesdeauville/%E3%80%90%E6%9D%B1%E6%96%B9Vocal%EF%BC%8FEurobeat%E3%80%91%20STARVING%20RAVEN%20%E3%80%8CSOUND%20HOLIC%E3%80%8D.mp3",
        AlbumCoverPath = "https://open-tracks.vercel.app/Assets/ncs/2.jpg",
        Artist = "nana takahashi, jp remixi"
      });
      observableCollection2.Add(new TrackItem()
      {
        Title = "NIGHTS OF NIGHTS",
        AudioPath = "https://ia601500.us.archive.org/12/items/all-night-of-knights-cool-and-create/01.%20Night%20of%20Knights.mp3",
        AlbumCoverPath = "https://dl.marmak.net.pl/images/009%20Sound%20System/009%20Sound%20System.jpg",
        Artist = "Flowering Night Remix"
      });
      observableCollection2.Add(new TrackItem()
      {
        Title = "IGNITE THE POWER",
        AudioPath = "https://ia800906.us.archive.org/29/items/penseesdeauville/IGNITE%20THE%20POWER.mp3",
        AlbumCoverPath = "https://i1.sndcdn.com/artworks-000587244338-vn5orc-t500x500.jpg",
        Artist = "nana takahashi, th remix"
      });
      observableCollection2.Add(new TrackItem()
      {
        Title = "00 HEAVEN",
        AudioPath = "https://ia800906.us.archive.org/29/items/penseesdeauville/%E3%80%90%E6%9D%B1%E6%96%B9Vocal%EF%BC%8FEurobeat%E3%80%91%2000%20HEAVEN%20%E3%80%8CSOUND%20HOLIC%E3%80%8D.mp3",
        AlbumCoverPath = "https://i1.sndcdn.com/artworks-000567230594-q8qfvo-t1080x1080.jpg",
        Artist = "nana takahashi, th remix"
      });
      observableCollection2.Add(new TrackItem()
      {
        Title = "FIRST STORM",
        AudioPath = "https://ia800906.us.archive.org/29/items/penseesdeauville/FIRST%20STORM%20%EF%BD%9C%20%F0%9D%90%BB%F0%9D%91%8E%F0%9D%91%A1%F0%9D%91%A0%F0%9D%91%A2%F0%9D%91%9B%F0%9D%91%92%20%F0%9D%91%80%F0%9D%91%96%F0%9D%91%98%F0%9D%91%A2%20%EF%BD%9C%20No%20Copyright%20%F0%9F%8E%B5.mp3",
        AlbumCoverPath = "https://dl.marmak.net.pl/images/009%20Sound%20System/009%20Sound%20System.jpg",
        Artist = "Hatsune Miku"
      });
      observableCollection2.Add(new TrackItem()
      {
        Title = "HAZY MOON",
        AudioPath = "https://ia600906.us.archive.org/29/items/penseesdeauville/Hazy%20Moon%20-%20Hatsune%20Miku%20%28%20no%20copyright%20music%20%29.mp3",
        AlbumCoverPath = "https://i1.sndcdn.com/artworks-000587244338-vn5orc-t500x500.jpg",
        Artist = "Hatsune Miku"
      });
      observableCollection2.Add(new TrackItem()
      {
        Title = "Ievan Polkka (Sped up)",
        AudioPath = "https://ia800906.us.archive.org/29/items/penseesdeauville/Ievan%20Polkka%20Song%20-%20Hatsune%20Miku%20%28Copyright%20Free%20Song%29.mp3",
        AlbumCoverPath = "https://i1.sndcdn.com/artworks-000567230594-q8qfvo-t1080x1080.jpg",
        Artist = "Hatsune Miku"
      });
      observableCollection2.Add(new TrackItem()
      {
        Title = "DIVE INTO STREAM",
        AudioPath = "https://ia600906.us.archive.org/29/items/penseesdeauville/dintos.mp3",
        AlbumCoverPath = "https://open-tracks.vercel.app/Assets/ncs/2.jpg",
        Artist = "Hatsune Miku"
      });
      observableCollection2.Add(new TrackItem()
      {
        Title = "Nekozilla",
        AudioPath = "https://ia803109.us.archive.org/27/items/generic-nekozilla-lfz-remix-_different-heaven_-6189037-wEqcGyYvm/nekozilla-lfz-remix-_different-heaven_-6189037-wEqcGyYvm-nekozilla-lfz-remix-_different-heaven_-6189037-wEqcGyYvm.mp3",
        Artist = "LFZ Remix"
      });
      observableCollection2.Add(new TrackItem()
      {
        Title = "Trinity",
        AudioPath = "https://ia801302.us.archive.org/2/items/009SoundSystemTrinityFullVersion/009%20Sound%20System%20-%20%21%20Trinity%20%28Full%20Version%29.mp3",
        AlbumCoverPath = "https://dl.marmak.net.pl/images/009%20Sound%20System/009%20Sound%20System.jpg",
        Artist = "009 sound system"
      });
      observableCollection2.Add(new TrackItem()
      {
        Title = "With a spirit",
        AudioPath = "https://ia600705.us.archive.org/24/items/009-sound-system-cd-rip/10%20-%20009%20Sound%20System%20-%20With%20A%20Spirit.mp3",
        AlbumCoverPath = "https://dl.marmak.net.pl/images/009%20Sound%20System/009%20Sound%20System.jpg",
        Artist = "009 sound system"
      });
      observableCollection2.Add(new TrackItem()
      {
        Title = "Miracle Rose",
        AudioPath = "https://ia601000.us.archive.org/2/items/eva_20250816_202508/miraclerose.mp3",
        AlbumCoverPath = "https://static.zerochan.net/Initial.D.full.1184790.jpg",
        Artist = "Galla, いさみんチャンネル"
      });
      observableCollection2.Add(new TrackItem()
      {
        Title = "You",
        AudioPath = "https://ia804607.us.archive.org/31/items/ncsyt-13-kontinuum-lost-feat.-savoi-jjd-remix/NIVIRO%20-%20You%20%5BNCS%20Release%5D.mp3",
        AlbumCoverPath = "https://blogger.googleusercontent.com/img/b/R29vZ2xl/AVvXsEg6Apzv91t3j7f9FqJdrAnzXxAocDgI8E4IuWnpyPSBO7k5q837Ov2GWHxBTrhoLjU_Skp-xFGEyky2yuwNMsA_7Ht2GxjSZVg4O5D_Kp6d91LvVB_eQOn21LbnkGnJsA-5kftYf1D1uatc/w1200-h630-p-k-no-nu/street_fighter_ex3_cover.jpg",
        Artist = "NIVIRO"
      });
      observableCollection2.Add(new TrackItem()
      {
        Title = "Save Another day for me",
        AudioPath = "https://ia902302.us.archive.org/35/items/de-leo-save-another-day-for-me/De%20Leo%20-%20Save%20Another%20Day%20For%20Me.mp3",
        AlbumCoverPath = "https://i.discogs.com/fFGJBrZI_jyGAdrprDaThtBt0gjecrn2Tgt9RtHiFME/rs:fit/g:sm/q:90/h:400/w:397/czM6Ly9kaXNjb2dz/LWRhdGFiYXNlLWlt/YWdlcy9SLTE0MTUw/MjEzLTE1Njg3NjMw/NDMtNDczMC5qcGVn.jpeg",
        Artist = "De Leo"
      });
      observableCollection2.Add(new TrackItem()
      {
        Title = "Still I love you (NC)",
        AudioPath = "https://ia600905.us.archive.org/21/items/nightcoremixes10243/stilliloveyou.mp3",
        AlbumCoverPath = "https://static.wikia.nocookie.net/school-days/images/d/dc/Cover.jpg/revision/latest?cb=20200511110231",
        Artist = "JPREMIXI"
      });
      observableCollection2.Add(new TrackItem()
      {
        Title = "イノセント・ブルー (NC)",
        AudioPath = "https://ia803203.us.archive.org/33/items/eva_20250816/pureblue.mp3",
        AlbumCoverPath = "https://static.wikia.nocookie.net/school-days/images/d/dc/Cover.jpg/revision/latest?cb=20200511110231",
        Artist = "JP REMIXI"
      });
      observableCollection2.Add(new TrackItem()
      {
        Title = "紅蓮華 EUROBEAT Remix",
        AudioPath = "https://ia903203.us.archive.org/33/items/eva_20250816/gurenge.mp3",
        AlbumCoverPath = "https://i1.sndcdn.com/artworks-000587244338-vn5orc-t500x500.jpg",
        Artist = "Turbo"
      });
      observableCollection2.Add(new TrackItem()
      {
        Title = "A Cruel Angels Thesis Eurobeat Remix",
        AudioPath = "https://ia803203.us.archive.org/33/items/eva_20250816/eva.mp3",
        AlbumCoverPath = "https://i1.sndcdn.com/artworks-000567230594-q8qfvo-t1080x1080.jpg",
        Artist = "Turbo"
      });
      observableCollection2.Add(new TrackItem()
      {
        Title = "立ち上がリーヨ!",
        AudioPath = "https://ia803203.us.archive.org/33/items/eva_20250816/%E7%AB%8B%E3%81%A1%E4%B8%8A%E3%81%8C%E3%83%AA%E3%83%BC%E3%83%A8.mp3",
        AlbumCoverPath = "https://cdn.aniplaylist.com/thumbnails/aE93evFQvpEFc1CeKHTXdgvVrKnPLEoJdizRE2MJ.jpeg",
        Artist = "T-Pistonz, pugcat"
      });
      observableCollection2.Add(new TrackItem()
      {
        Title = "Mes pensées le soir (V1)",
        AudioPath = "https://ia600906.us.archive.org/29/items/penseesdeauville/penseesdeauville.mp3",
        AlbumCoverPath = "https://source.boomplaymusic.com/group10/M00/06/29/ffeab3b4a91f46daa89f504b1958261d_320_320.jpg",
        Artist = "Smoking Deauville"
      });
      observableCollection2.Add(new TrackItem()
      {
        Title = "caramelldansen (nightcore)",
        AudioPath = "https://ia801000.us.archive.org/2/items/eva_20250816_202508/caramelldansen.mp3",
        AlbumCoverPath = "https://i.kfs.io/album/global/68318497,0v1/fit/500x500.jpg",
        Artist = "Caramella Girls, KidousRemix"
      });
      observableCollection2.Add(new TrackItem()
      {
        Title = "Adventure",
        AudioPath = "https://ia600900.us.archive.org/25/items/allaround_202508/Adventure.mp3",
        AlbumCoverPath = "https://open-tracks.vercel.app/Assets/ncs/3.jpg",
        Artist = "JJD"
      });
      observableCollection2.Add(new TrackItem()
      {
        Title = "Arrow",
        AudioPath = "https://ia800900.us.archive.org/25/items/allaround_202508/Arrow.mp3",
        AlbumCoverPath = "https://open-tracks.vercel.app/Assets/ncs/1.jpg",
        Artist = "Jim Yosef"
      });
      observableCollection2.Add(new TrackItem()
      {
        Title = "Blank",
        AudioPath = "https://ia903209.us.archive.org/18/items/DisfigureBlankNCSRelease/Disfigure%20-%20Blank%20%5BNCS%20Release%5D.mp3",
        AlbumCoverPath = "https://open-tracks.vercel.app/Assets/ncs/2.jpg",
        Artist = "Disfigure"
      });
      observableCollection2.Add(new TrackItem()
      {
        Title = "Candyland",
        AudioPath = "https://ia903205.us.archive.org/31/items/TobuCandylandNCSRelease/Tobu%20-%20Candyland%20%5BNCS%20Release%5D.mp3",
        AlbumCoverPath = "https://open-tracks.vercel.app/Assets/ncs/4.jpeg",
        Artist = "Tobu"
      });
      observableCollection2.Add(new TrackItem()
      {
        Title = "Cetus",
        AudioPath = "https://ia601902.us.archive.org/10/items/LenskoCetusNCSRelease/Lensko%20-%20Cetus%20%5BNCS%20Release%5D.mp3",
        AlbumCoverPath = "https://open-tracks.vercel.app/Assets/ncs/5.jpg",
        Artist = "Lensko"
      });
      observableCollection2.Add(new TrackItem()
      {
        Title = "Circles",
        AudioPath = "https://ia903206.us.archive.org/7/items/LenskoCirclesNCSRelease/Lensko%20-%20Circles%20%5BNCS%20Release%5D.mp3",
        AlbumCoverPath = "https://open-tracks.vercel.app/Assets/ncs/5.jpg",
        Artist = "Lensko"
      });
      observableCollection2.Add(new TrackItem()
      {
        Title = "Seven",
        AudioPath = "https://ia801904.us.archive.org/8/items/TobuSevenNCSRelease/Tobu%20-%20Seven%20%5BNCS%20Release%5D.mp3",
        AlbumCoverPath = "https://open-tracks.vercel.app/Assets/ncs/1.jpg",
        Artist = "Tobu"
      });
      observableCollection2.Add(new TrackItem()
      {
        Title = "Energy",
        AudioPath = "https://ia601904.us.archive.org/4/items/ElektronomiaEnergyNCSRelease/Elektronomia%20-%20Energy%20%5BNCS%20Release%5D.mp3",
        AlbumCoverPath = "https://open-tracks.vercel.app/Assets/ncs/5.jpg",
        Artist = "Elektronomia"
      });
      observableCollection2.Add(new TrackItem()
      {
        Title = "Entropy",
        AudioPath = "https://ia803203.us.archive.org/34/items/DistrionAlexSkrindoEntropyNCSRelease/Distrion%20%20Alex%20Skrindo%20-%20Entropy%20%5BNCS%20Release%5D.mp3",
        AlbumCoverPath = "https://open-tracks.vercel.app/Assets/ncs/5.jpg",
        Artist = "Distrion, Alex"
      });
      observableCollection2.Add(new TrackItem()
      {
        Title = "Good Times",
        AudioPath = "https://ia801708.us.archive.org/1/items/soundcloud-817542118/817542118.mp3",
        AlbumCoverPath = "https://open-tracks.vercel.app/Assets/ncs/1.jpg",
        Artist = "Tobu"
      });
      observableCollection2.Add(new TrackItem()
      {
        Title = "Hellcat",
        AudioPath = "https://ia801903.us.archive.org/30/items/DesmeonHellcatNCSRelease/Desmeon%20-%20Hellcat%20%5BNCS%20Release%5D.mp3",
        AlbumCoverPath = "https://open-tracks.vercel.app/Assets/ncs/3.jpg",
        Artist = "Desmon"
      });
      observableCollection2.Add(new TrackItem()
      {
        Title = "Hello",
        AudioPath = "https://ia801209.us.archive.org/1/items/OMFGHello_20150908/OMFG%20-%20Hello.mp3",
        AlbumCoverPath = "https://i1.sndcdn.com/artworks-000101331401-iecrgv-t500x500.jpg",
        Artist = "OMFG"
      });
      observableCollection2.Add(new TrackItem()
      {
        Title = "High",
        AudioPath = "https://ia801902.us.archive.org/26/items/JPBHighNCSRelease/JPB%20-%20High%20%5BNCS%20Release%5D.mp3",
        AlbumCoverPath = "https://open-tracks.vercel.app/Assets/ncs/2.jpg",
        Artist = "JPB"
      });
      observableCollection2.Add(new TrackItem()
      {
        Title = "Higher",
        AudioPath = "https://ia800306.us.archive.org/17/items/TobuHigher/Tobu%20-%20Higher.mp3",
        AlbumCoverPath = "https://open-tracks.vercel.app/Assets/ncs/2.jpg",
        Artist = "Tobu"
      });
      observableCollection2.Add(new TrackItem()
      {
        Title = "Hope",
        AudioPath = "https://ia803207.us.archive.org/32/items/TobuHopeNCSRelease_201612/Tobu%20-%20Hope%20%5BNCS%20Release%5D.mp3",
        AlbumCoverPath = "https://open-tracks.vercel.app/Assets/ncs/2.jpg",
        Artist = "Tobu"
      });
      observableCollection2.Add(new TrackItem()
      {
        Title = "Limitless",
        AudioPath = "https://ia804607.us.archive.org/31/items/ncsyt-13-kontinuum-lost-feat.-savoi-jjd-remix/Elektronomia%20-%20Limitless%20%5BNCS%20Release%5D.mp3",
        AlbumCoverPath = "https://open-tracks.vercel.app/Assets/ncs/5.jpg",
        Artist = "Elektronomia"
      });
      observableCollection2.Add(new TrackItem()
      {
        Title = "Monody",
        AudioPath = "https://ia801304.us.archive.org/31/items/gdps-2.2-song-652927/652927.mp3",
        AlbumCoverPath = "https://open-tracks.vercel.app/Assets/ncs/2.jpg",
        Artist = "TheFatRat"
      });
      observableCollection2.Add(new TrackItem()
      {
        Title = "Puzzle",
        AudioPath = "https://ia801702.us.archive.org/8/items/soundcloud-317698312/317698312.mp3",
        AlbumCoverPath = "https://open-tracks.vercel.app/Assets/ncs/4.jpeg",
        Artist = "RetroVision"
      });
      observableCollection2.Add(new TrackItem()
      {
        Title = "Windfall",
        AudioPath = "https://ia801203.us.archive.org/21/items/gdps-2.2-song-621135/621135.mp3",
        AlbumCoverPath = "https://open-tracks.vercel.app/Assets/ncs/1.jpg",
        Artist = "TheFatRat"
      });
      observableCollection2.Add(new TrackItem()
      {
        Title = "Xenogenesis",
        AudioPath = "https://ia800506.us.archive.org/12/items/gdps-2.2-song-621136/621136.mp3",
        AlbumCoverPath = "https://open-tracks.vercel.app/Assets/ncs/5.jpg",
        Artist = "TheFatRat"
      });
      observableCollection2.Add(new TrackItem()
      {
        Title = "Let's Go!",
        AudioPath = "https://ia803207.us.archive.org/30/items/LenskoLetsGoNCSRelease/Lensko%20-%20Lets%20Go%21%20%5BNCS%20Release%5D.mp3",
        AlbumCoverPath = "https://open-tracks.vercel.app/Assets/ncs/5.jpg",
        Artist = "Lensko"
      });
      observableCollection2.Add(new TrackItem()
      {
        Title = "Nekozilla",
        AudioPath = "https://ia803207.us.archive.org/20/items/whats212139all/Nekozilla.mp3",
        AlbumCoverPath = "https://open-tracks.vercel.app/Assets/ncs/2.jpg",
        Artist = "Different Heaven"
      });
      observableCollection2.Add(new TrackItem()
      {
        Title = "Ahrix - Nova",
        AudioPath = "https://ia803201.us.archive.org/1/items/AhrixNovaNCSRelease_201612/Ahrix%20-%20Nova%20%5BNCS%20Release%5D.mp3",
        AlbumCoverPath = "https://open-tracks.vercel.app/Homework%20Assets/n.jpg",
        Artist = "Ahrix"
      });
      observableCollection2.Add(new TrackItem()
      {
        Title = "Popsicle",
        AudioPath = "https://ia902900.us.archive.org/30/items/LFZPopsicleOriginalMix/LFZ%20-%20Popsicle%20%28Original%20Mix%29.mp3",
        AlbumCoverPath = "https://open-tracks.vercel.app/Assets/ncs/5.jpg",
        Artist = "LFZ"
      });
      observableCollection2.Add(new TrackItem()
      {
        Title = "Sky High",
        AudioPath = "https://ia804607.us.archive.org/31/items/ncsyt-13-kontinuum-lost-feat.-savoi-jjd-remix/Elektronomia%20-%20Sky%20High%20%5BNCS%20Release%5D.mp3",
        AlbumCoverPath = "https://open-tracks.vercel.app/Assets/ncs/5.jpg",
        Artist = "Elektronomia"
      });
      observableCollection2.Add(new TrackItem()
      {
        Title = "Sun burst",
        AudioPath = "https://ia903205.us.archive.org/35/items/TobuItroSunburstNCSRelease/Tobu%20%20Itro%20-%20Sunburst%20%5BNCS%20Release%5D.mp3",
        AlbumCoverPath = "https://open-tracks.vercel.app/Homework%20Assets/t.jpg",
        Artist = "Tobu"
      });
      observableCollection2.Add(new TrackItem()
      {
        Title = "Symbolism",
        AudioPath = "https://ia801705.us.archive.org/24/items/electro-light-symbolism-ncs-release/Electro-Light%20-%20Symbolism%20%5BNCS%20Release%5D.mp3",
        AlbumCoverPath = "https://open-tracks.vercel.app/Assets/ncs/5.jpg",
        Artist = "Electro-Light"
      });
      observableCollection2.Add(new TrackItem()
      {
        Title = "Unity",
        AudioPath = "https://ia801205.us.archive.org/28/items/gdps-2.2-song-621134/621134.mp3",
        AlbumCoverPath = "https://open-tracks.vercel.app/Homework%20Assets/ft.jpg",
        Artist = "TheFatRat"
      });
      observableCollection2.Add(new TrackItem()
      {
        Title = "Fluxxwave (REMIX)",
        AudioPath = "https://ia801000.us.archive.org/2/items/eva_20250816_202508/fluxrm1.mp3",
        AlbumCoverPath = "https://i.scdn.co/image/ab67616d0000b273aa00c31473fe4542a1ed81a2",
        Artist = "clovis reyes, EPROVES REMIX"
      });
      observableCollection2.Add(new TrackItem()
      {
        Title = "WAKEUP (REMIX)",
        AudioPath = "https://ia801000.us.archive.org/2/items/eva_20250816_202508/wakeup1.mp3",
        AlbumCoverPath = "https://i.scdn.co/image/ab67616d0000b273a3a0d9665cc88b29b1d69f8f",
        Artist = "MOONDEITY, EPROVES REMIX"
      });
      observableCollection2.Add(new TrackItem()
      {
        Title = "IMMACULATE (REMIX)",
        AudioPath = "https://ia801000.us.archive.org/2/items/eva_20250816_202508/immaculateremix.mp3",
        AlbumCoverPath = "https://i.scdn.co/image/ab67616d0000b273a3a0d9665cc88b29b1d69f8f",
        Artist = "ASPHALT REMIX"
      });
      observableCollection2.Add(new TrackItem()
      {
        Title = "Maybe Tonight Remix",
        AudioPath = "https://ia801000.us.archive.org/2/items/eva_20250816_202508/maybetonight%20%282%29.mp3",
        AlbumCoverPath = "https://i1.sndcdn.com/artworks-000123477049-b0o5t1-t500x500.jpg",
        Artist = "MAKO & SAYUKI"
      });
      observableCollection2.Add(new TrackItem()
      {
        Title = "Lunar Abyss",
        AudioPath = "https://ia801304.us.archive.org/19/items/gdps-2.2-song-645631/645631.mp3",
        AlbumCoverPath = "https://i.scdn.co/image/ab67616d0000b273f0d6546794cb5d0b5d41588b",
        Artist = "Lchavasse"
      });
      observableCollection2.Add(new TrackItem()
      {
        Title = "All Around",
        AudioPath = "https://ia600900.us.archive.org/25/items/allaround_202508/allaround.mp3",
        AlbumCoverPath = "https://static.wikia.nocookie.net/eurobeat/images/6/63/EYCA-12185-6_Cover.jpg/revision/latest/scale-to-width/360?cb=20220314234118",
        Artist = "Lia"
      });
      observableCollection2.Add(new TrackItem()
      {
        Title = "Secret Love",
        AudioPath = "https://finaldistance.net/mp3/Initial%20D%20Fourth%20Stage%20D%20Selection%20%2B/Nutty%20-%20Secret%20Love.mp3",
        AlbumCoverPath = "https://static.wikia.nocookie.net/eurobeat/images/4/4c/Initial_D_Vocal_Battle_Special_feat_Takahashi_Bros_Red_Suns.jpg/revision/latest?cb=20210515170210",
        Artist = "Nutty"
      });
      observableCollection2.Add(new TrackItem()
      {
        Title = "Don't Go Baby",
        AudioPath = "https://finaldistance.net/mp3/Initial%20D%20Fourth%20Stage%20D%20Selection%20%2B/Maiko%20-%20Don%60t%20Go%20Baby.mp3",
        AlbumCoverPath = "https://static.wikia.nocookie.net/eurobeat/images/4/4c/Initial_D_Vocal_Battle_Special_feat_Takahashi_Bros_Red_Suns.jpg/revision/latest?cb=20210515170210",
        Artist = "Maiko"
      });
      observableCollection2.Add(new TrackItem()
      {
        Title = "CATATONIC",
        AudioPath = "https://ia601701.us.archive.org/11/items/stilliloveyou/CATATONIC%20-%20fatestchan.mp3",
        AlbumCoverPath = "https://i.scdn.co/image/ab67616d0000b273aa00c31473fe4542a1ed81a2",
        Artist = "fatestchan"
      });
      observableCollection2.Add(new TrackItem()
      {
        Title = "ROCKIN' Hardcore",
        AudioPath = "https://ia804506.us.archive.org/1/items/initial-d-5th-stage-soundtrack-rockin-hardcore-reupload_202107/Initial%20D%205th%20Stage%20Soundtrack%20-%20Rockin%27%20Hardcore%20%28REUPLOAD%29.mp3",
        Artist = "Fastway"
      });
      observableCollection2.Add(new TrackItem()
      {
        Title = "PROJECT III Remix",
        AudioPath = "https://ia600906.us.archive.org/29/items/penseesdeauville/Initial%20D%205th%20Stage%20Sound%20File%20-%20Project%20D%20III.mp3",
        AlbumCoverPath = "https://i.scdn.co/image/ab67616d0000b273f0d6546794cb5d0b5d41588b",
        Artist = "ARYHM Remix"
      });
      this.AllTracks = observableCollection2;
      this.FilterTracks("");
    }

    private void PlayTrack(TrackItem track)
    {
      if (track == null || string.IsNullOrEmpty(track.AudioPath))
        return;
        
      // Use modern UWP MediaPlayer instead of legacy MediaPlayerLauncher
      try
      {
          var mediaSource = MediaSource.CreateFromUri(new Uri(track.AudioPath));
          var mediaPlayer = new MediaPlayer();
          mediaPlayer.Source = mediaSource;
          mediaPlayer.Play();
      }
      catch (Exception ex)
      {
          // Handle any errors in playing the track
          System.Diagnostics.Debug.WriteLine($"Error playing track: {ex.Message}");
      }
    }

    public void FilterTracks(string query)
    {
      if (query == null)
        query = "";
      query = query.ToLower();
      List<TrackItem> list = new List<TrackItem>();
      foreach (TrackItem allTrack in (Collection<TrackItem>) this.AllTracks)
      {
        if (allTrack.Title != null && allTrack.Title.ToLower().Contains(query) || allTrack.Artist != null && allTrack.Artist.ToLower().Contains(query))
          list.Add(allTrack);
      }
      this.FilteredTracks = new ObservableCollection<TrackItem>(list);
      this.OnPropertyChanged("FilteredTracks");
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string propertyName)
    {
      PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
      if (propertyChanged == null)
        return;
      propertyChanged((object) this, new PropertyChangedEventArgs(propertyName));
    }
  }
}