---
marp: true
theme: default
paginate: true
size: 16:9
style: |
  section, section * {
    box-sizing: border-box;
  }

  section {
    display: flex;
    flex-direction: column;
    padding: 0.7rem;
    font-size: 0.8rem;
    height: 100%;
    overflow: hidden;
  }
  
  .persona-header {
    text-align: center;
    margin-bottom: 0.8rem;
    flex-shrink: 0;
  }
  
  .persona-title {
    font-size: 2rem;
    font-weight: bold;
    color: #00838F;
    margin: 0;
    text-transform: uppercase;
    letter-spacing: 1px;
  }
  
  .persona-subtitle {
    font-size: 0.8rem;
    color: #666;
    margin: 0.2rem 0 0 0;
    text-transform: uppercase;
    letter-spacing: 1px;
  }
  
  .persona-content {
    display: flex;
    gap: 1rem;
    flex: 1;
    min-height: 0;
    align-items: stretch;
    height: 100%;
  }

  .profile-column {
    flex: 0 0 260px;
    display: flex;
    flex-direction: column;
    gap: 0.6rem;
    height: 100%;
    min-height: 0;
    max-height: 100%;
    align-self: stretch;
    overflow: hidden;
  }
  
  .profile-section {
    background: white;
    border: 3px solid #00838F;
    border-radius: 10px;
    padding: 0.6rem 0.75rem;
    text-align: center;
    box-shadow: 0 2px 6px rgba(0,0,0,0.1);
    display: flex;
    flex-direction: column;
    gap: 0.5rem;
    flex: 1 1 0;
    min-height: 0;
    overflow: hidden;
  }
  
  .sample-text {
    font-size: 0.55rem;
    color: #999;
    margin-bottom: 0.4rem;
    background: #f5f5f5;
    padding: 0.1rem 0.3rem;
    border-radius: 3px;
  }
  
  .profile-section img {
    width: 120px;
    height: 120px;
    border-radius: 50%;
    border: 2px solid #00838F;
    margin: 0 auto;
    flex-shrink: 0;
    object-fit: cover;
  }
  
  .persona-name {
    font-size: 1.1rem;
    font-weight: bold;
    color: #00838F;
    margin: 0.2rem 0 0.1rem 0;
    text-transform: uppercase;
    line-height: 1.0;
    flex-shrink: 0;
  }
  
  .job-title {
    color: #666;
    font-weight: bold;
    margin-bottom: 0.4rem;
    font-size: 0.75rem;
    line-height: 1.0;
    flex-shrink: 0;
  }
  
  .personal-data {
    flex: 1 1 0;
    overflow-y: auto;
    padding-right: 0.2rem;
    display: flex;
    flex-direction: column;
    gap: 0.4rem;
    min-height: 4rem;
  }

  .personal-data > div {
    background: transparent;
    border-radius: 0;
    padding: 0;
    border-left: none;
    box-shadow: none;
  }

  .personal-data h4 {
    color: #00838F;
    font-size: 0.75rem;
    margin: 0;
    text-transform: uppercase;
    text-align: left;
    letter-spacing: 0.5px;
  }

  .persona-facts {
    display: flex;
    flex-direction: column;
    gap: 0.25rem;
  }

  .data-item {
    display: flex;
    align-items: flex-start;
    gap: 0.35rem;
    text-align: left;
    font-size: 0.68rem;
    line-height: 1.2;
    color: #1f1f1f;
  }

  .data-icon {
    font-size: 0.8rem;
    line-height: 1;
    flex-shrink: 0;
    margin-top: 0.1rem;
  }

  .data-item span:last-child {
    flex: 1;
  }

  .personal-data ul {
    text-align: left;
    font-size: 0.7rem;
    line-height: 1.2;
    margin: 0;
    padding-left: 0.9rem;
  }

  .personal-data li {
    margin-bottom: 0.2rem;
    color: #333;
  }
  
  .content-columns {
    display: flex;
    flex: 1;
    gap: 1.2rem;
    min-height: 0;
  }
  
  .content-single-column {
    flex: 1;
    display: flex;
    flex-direction: column;
    gap: 0.6rem;
    min-height: 0;
  }
  
  .content-column {
    flex: 1;
    display: flex;
    flex-direction: column;
    min-height: 0;
  }
  
  .content-box {
    background: rgba(0, 131, 143, 0.05);
    border-radius: 6px;
    padding: 0.7rem;
    margin-bottom: 0.6rem;
    border-left: 3px solid #00838F;
    flex-shrink: 0;
  }
  
  .section-title {
    font-size: 0.9rem;
    font-weight: bold;
    color: #00838F;
    margin: 0 0 0.4rem 0;
    text-transform: uppercase;
    line-height: 1.1;
  }
  
  .content-box ul {
    margin: 0;
    padding-left: 0.8rem;
  }
  
  .content-box li {
    margin-bottom: 0.2rem;
    line-height: 1.1;
    color: #333;
    font-size: 0.75rem;
  }
  
  .skills-box {
    background: rgba(0, 131, 143, 0.05);
    border-radius: 8px;
    padding: 1rem;
    border-left: 3px solid #00838F;
    flex: 1;
    min-height: 0;
  }
  
  .skill-item {
    display: flex;
    justify-content: space-between;
    align-items: center;
    margin: 0.4rem 0;
    font-size: 0.8rem;
  }
  
  .stars {
    color: #00838F;
    font-size: 0.9rem;
  }
  
  .systems-grid {
    display: flex;
    justify-content: space-between;
    margin-top: 0.6rem;
    gap: 0.3rem;
  }
  
  .system-badge {
    background: #00838F;
    color: white;
    padding: 0.3rem 0.5rem;
    border-radius: 4px;
    font-weight: bold;
    font-size: 0.7rem;
    text-align: center;
    flex: 1;
  }
  
  .logo-container {
    display: flex;
    flex-direction: column;
    align-items: center;
    gap: 0.4rem;
    flex-shrink: 0;
    height: auto;
    padding: 0.3rem 0;
  }

  .logo-container img {
    max-width: 200px;
    height: auto;
    object-fit: contain;
    flex-shrink: 0;
  }

  .logo-container img:first-child {
    max-height: 28px;
  }

  .logo-container img:last-child {
    max-height: 20px;
  }

---

# UKHSA DATA PROTECTION OFFICER

<div class="persona-content">

<div class="profile-column">
  <div class="profile-section">
  <img src="../images/placeholder.jpg" alt="Profile Photo" />
    <h2 class="persona-name">Rachel Goldstein</h2>
    <p class="job-title">Data Protection Officer & Head of IG, UKHSA</p>
    <div class="personal-data">
      <div>
        <h4>Professional Data</h4>
        <div class="persona-facts">
          <div class="data-item"><span class="data-icon">👤</span><span>Solicitor (non-practising), IAPP CIPP/E and CIPM-certified</span></div>
          <div class="data-item"><span class="data-icon">📍</span><span>UK-based</span></div>
          <div class="data-item"><span class="data-icon">💼</span><span>UKHSA Information Governance</span></div>
        </div>
      </div>
      <div>
        <h4>Role & Background</h4>
        <ul>
          <li>Statutory DPO under UK GDPR for UKHSA</li>
          <li>Former NHS IG manager with extensive ICO experience</li>
          <li>Hard-of-hearing in noisy environments; uses Teams live captions</li>
          <li>Manages DPIAs, breach response, ROPA, and Subject Access Requests</li>
        </ul>
      </div>
    </div>
  </div>

  <div class="logo-container">
    <img src="../images/ukhsa-logo.png" alt="UKHSA logo" />
    <img src="../images/microsoft-logo.png" alt="Microsoft logo" />
  </div>
</div>

<div class="content-single-column">
  
  <div class="content-box">
    <h3 class="section-title">Goals & Desired Outcomes</h3>
    <ul>
          <li>Redesign meets UK GDPR / DPA 2018 and NHS DSPT standards</li>
          <li>No ICO enforcement; DPIA accepted; no breaches from the new service</li>
          <li>Privacy notice the applicant actually reads — plain English</li>
    </ul>
  </div>
  
  <div class="content-box">
    <h3 class="section-title">Wants & Needs</h3>
    <ul>
          <li>Strong audit logging, role-based access, and retention enforcement</li>
          <li>Secure transit (TLS) and minimum-necessary data collection</li>
          <li>Briefings on new design's data flows and processor contracts</li>
    </ul>
  </div>

  <div class="content-box">
    <h3 class="section-title">Pain Points & Frustrations</h3>
    <ul>
          <li>PDF-and-email workflow scatters personal data across mailboxes</li>
          <li>No structured retention policy on the email artefacts that are the audit trail</li>
          <li>Applicants asked for personal data (e.g. mobile numbers) without clear lawful basis</li>
    </ul>
  </div>

</div>

</div>
