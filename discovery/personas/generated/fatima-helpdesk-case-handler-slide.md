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
    position: relative;
  }

  section > h1 {
    margin-left: 1.5cm !important;
    margin-top: 50px !important;
    position: relative;
    top: 20px;
  }
  
  .persona-header {
    text-align: center;
    margin-bottom: 0.8rem;
    flex-shrink: 0;
  }
  
  .persona-title {
    font-size: 2rem;
    font-weight: bold;
    color: #007C91;
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
    margin-top: 0.6cm;
    min-height: 0;
    align-items: flex-start;
    height: 100%;
  }

  .profile-column {
    flex: 0 0 260px;
    display: flex;
    flex-direction: column;
    gap: 0.6rem;
    margin-left: 1cm;
    margin-top: 0;
    height: 100%;
    min-height: 0;
    max-height: 100%;
    align-self: stretch;
    overflow: hidden;
  }
  
  .profile-section {
    background: white;
    border: 3px solid #007C91;
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
    background: #007C91;
    padding: 0.1rem 0.3rem;
    border-radius: 3px;
  }
  
  .profile-section img {
    width: 120px;
    height: 120px;
    border-radius: 50%;
    border: 2px solid #007C91;
    margin: 0 auto;
    flex-shrink: 0;
    object-fit: cover;
  }
  
  .persona-name {
    font-size: 1.1rem;
    font-weight: bold;
    color: #007C91;
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
    color: #007C91;
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
    margin-top: 0;
    min-height: 0;
  }
  
  .content-column {
    flex: 1;
    display: flex;
    flex-direction: column;
    min-height: 0;
  }
  
  .content-box {
    background: rgba(0, 94, 184, 0.05);
    border-radius: 6px;
    padding: 0.7rem;
    margin-bottom: 0.6rem;
    border-left: 3px solid #007C91;
    flex-shrink: 0;
  }

  .boxed-section {
    border: 3px solid #007C91;
  }

  .section-title {
    font-size: 0.9rem;
    font-weight: bold;
    color: #007C91;
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
    background: rgba(0, 94, 184, 0.05);
    border-radius: 8px;
    padding: 1rem;
    border-left: 3px solid #007C91;
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
    color: #007C91;
    font-size: 0.9rem;
  }
  
  .systems-grid {
    display: flex;
    justify-content: space-between;
    margin-top: 0.6rem;
    gap: 0.3rem;
  }
  
  .system-badge {
    background: #007C91;
    color: white;
    padding: 0.3rem 0.5rem;
    border-radius: 4px;
    font-weight: bold;
    font-size: 0.7rem;
    text-align: center;
    flex: 1;
  }
  
  .ukhsa-logo-top-right {
    position: absolute;
    top: 0.5cm;
    right: 0.7cm;
    z-index: 2;
  }

  .ukhsa-logo-top-right img {
    height: 45px;
    width: auto;
    object-fit: contain;
  }

---

# IMMFORM HELPDESK (FALLBACK)

<div class="ukhsa-logo-top-right">
  <img src="../images/UKHSA-master-logo.jpg" alt="UKHSA logo" />
</div>

<div class="persona-content">

<div class="profile-column">
  <div class="profile-section">
  <img src="../images/fatima-helpdesk-case-handler.jpg" alt="Profile Photo" />
    <h2 class="persona-name">Fatima Osei</h2>
    <p class="job-title">Case Handler - ImmForm Support (Digitally-Assisted Fallback)</p>
    <div class="personal-data">
      <div>
        <h4>Professional Data</h4>
        <div class="persona-facts">
          <div class="data-item"><span class="data-icon">👤</span><span>3 years in ImmForm support; transitioned from manual helpdesk to assisted role</span></div>
          <div class="data-item"><span class="data-icon">📍</span><span>UKHSA National Office</span></div>
          <div class="data-item"><span class="data-icon">💼</span><span>ImmForm Support Team / Case Management</span></div>
        </div>
      </div>
      <div>
        <h4>Role & Background</h4>
        <ul>
          <li>ImmForm support case handler; newer member of team</li>
          <li>Works on complex registrations and fallback cases</li>
          <li>Handles edge cases: shared mailboxes, invalid codes, approval delays</li>
          <li>Recently upskilled on digital-assisted case management</li>
          <li>Supports primary helpdesk operators on difficult applications</li>
        </ul>
      </div>
    </div>
  </div>

</div>

<div class="content-single-column">
  
  <div class="content-box boxed-section goals-box">
    <h3 class="section-title">Goals & Desired Outcomes</h3>
    <ul>
          <li>Resolve complex registration cases efficiently and fairly</li>
          <li>Support users who face barriers with standard registration</li>
          <li>Maintain compliance while being flexible for edge cases</li>
          <li>Build expertise in handling difficult scenarios</li>
    </ul>
  </div>
  
  <div class="content-box boxed-section">
    <h3 class="section-title">Wants & Needs</h3>
    <ul>
          <li>Clear decision-making framework for edge cases</li>
          <li>Tools to verify identity and account details</li>
          <li>Access to Authorised Person contact information for escalation</li>
          <li>Digital workflow to track case status and decisions</li>
          <li>Training on regulatory requirements and compliance</li>
    </ul>
  </div>

  <div class="content-box boxed-section">
    <h3 class="section-title">Pain Points & Frustrations</h3>
    <ul>
          <li>No standardised process for handling edge cases</li>
          <li>Limited tools to verify user identity and account details</li>
          <li>Depends on escalation to seniors for difficult decisions</li>
          <li>Manual tracking makes it hard to report on case outcomes</li>
          <li>Difficult users or unclear requirements can get stuck indefinitely</li>
    </ul>
  </div>

</div>

</div>
